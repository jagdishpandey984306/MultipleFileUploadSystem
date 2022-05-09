using System;
using FileUpload.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileUpload.Database;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Linq;

namespace FileUpload.Services.FileUpload
{

    public class UploadFiles
    {
        public byte[] fileByte { get; set; }
        public string Extension { get; set; }
        public string fileName { get; set; }
    }

    public class FileUploadSystem : IFileUploadSystem
    {
        private readonly DBConnection _dBConnection;
        private readonly IConfiguration _configuration;
        public FileUploadSystem(DBConnection dBConnection, IConfiguration configuration)
        {
            _dBConnection = dBConnection;
            _configuration = configuration;
        }

        public async Task<ResponseMessage> UploadFile(FileUploadDetails param)
        {
            try
            {
                List<UploadFiles> arrFiles = new();
                if (param.files.Count > 0)
                {
                    var emailId = _configuration["emailId"].ToString();
                    var emailPassword = _configuration["emailPassword"].ToString();
                    foreach (var file in param.files)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
                        var extension = Path.GetExtension(file.FileName);
                        var data = new UploadFile
                        {
                            Extension = extension,
                            UploadDate = DateTime.Now
                        };
                        using (var dataStream = new MemoryStream())
                        {
                            await file.CopyToAsync(dataStream);
                            data.File = dataStream.ToArray();
                        }
                        _dBConnection.Add(data);

                        //attachements details
                        var isExist = _dBConnection.AttachementDetails.Where(p => p.ToMail.Trim() == param.ToEmail.Trim()).FirstOrDefault();
                        if (isExist == null)
                        {
                            var attachementDetails = new AttachementDetails()
                            {
                                FromMail = emailId,
                                ToMail = param.ToEmail,
                                SendDate = DateTime.Now,
                            };
                            _dBConnection.Add(attachementDetails);
                        }
                        _dBConnection.SaveChanges();
                        arrFiles.Add(new UploadFiles
                        {
                            fileByte = data.File,
                            Extension = extension,
                            fileName = fileName
                        });
                    }

                    //sender details
                    var sender = new AttachmentsFile
                    {
                        Attachments = arrFiles.Select(p => p.fileByte).ToArray(),
                        Extension = arrFiles.Select(p => p.Extension).ToArray(),
                        FileName = arrFiles.Select(p => p.fileName).ToArray(),
                        EmailId = emailId,
                        Password = emailPassword,
                        To = param.ToEmail
                    };

                    var result = EmailHelper.Sending(sender);
                    if (result == "Success")
                    {
                        return new ResponseMessage()
                        {
                            Code = "Success",
                            Message = "Email Send Successfully"
                        };
                    }
                    else
                    {
                        return new ResponseMessage()
                        {
                            Code = "Error",
                            Message = "Invalid File"
                        };
                    }
                }
                else
                {
                    return new ResponseMessage()
                    {
                        Code = "Error",
                        Message = "files nof found"
                    };
                }
            }
            catch (Exception ex)
            {
                throw await Task.Run(() => ex);
            }
        }


        public async Task<List<UploadFile>> List()
        {
            var list = await Task.Run(() => _dBConnection.UploadFile.ToList());
            return list ?? new List<UploadFile>();
        }

        public async Task<ResponseMessage> Delete(int id)
        {
            var data = await Task.Run(() => _dBConnection.UploadFile.Where(d => d.Id == id).First());
            _dBConnection.UploadFile.Remove(data);
            _dBConnection.SaveChanges();
            return new ResponseMessage()
            {
                Code = "Success",
                Message = "Deleted Successfully"
            };
        }
    }
}
