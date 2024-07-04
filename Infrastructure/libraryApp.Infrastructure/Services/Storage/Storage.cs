using libraryApp.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName,string fileName); // temsilci olusturduk
        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName,HasFile hasFileMethod, bool first = true)
        {
             

            string newFileName = await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName); // filename uzantısını alıyoruz
                string newFileName = string.Empty; // bu scope içerisindeki new file name baska scope da gecerli 

                if (first)
                {
                    string oldname = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = NameOperation.CharacterRegulatory(oldname);// karakter düzenlmesi yapıp geri gonderiyoruz
                }
                else
                {
                    newFileName = Path.GetFileNameWithoutExtension(fileName);


                    if (!string.IsNullOrEmpty(newFileName))
                    {
                        int lastChar = newFileName.LastIndexOf('-');

                        if (lastChar == -1 || lastChar == newFileName.Length - 1 || lastChar < newFileName.Length - 3)
                            newFileName += "-2";  // Son karakter '-' değilse, son karakterine '-2' 

                        else
                        {
                            // Son karakter '-' ise, yanındaki sayıyı 1 arttır                          
                            string prefix = newFileName.Substring(0, lastChar + 1);
                            string numberStr = newFileName.Substring(lastChar + 1);
                            if (int.TryParse(numberStr, out int number))
                            {
                                number++;
                                newFileName = prefix + number;
                            }
                            else
                            {
                                newFileName = prefix + "-2"; // -var numara yok

                            }
                        }
                    }

                }

                //if (File.Exists($"{path}\\{newFileName + extension}"))
                if (hasFileMethod(pathOrContainerName, newFileName + extension))
                    return await FileRenameAsync(pathOrContainerName, newFileName + extension,hasFileMethod, false);
                else
                    return newFileName + extension;
            });

            return newFileName;
        }
    }
}
