using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Ger3ahRepository : IGer3ahRepository
    {
        private readonly Ger3ahContext _context;
        private readonly IMapper _mapper;
        public Ger3ahRepository(Ger3ahContext context,
        IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetNumberOfPeboleInGer3ahDto> GetAllGer3ahNames()
        {
            var ger3ahNames = await _context.Ger3ahNames.Include(x => x.User).Where(x => !x.IsTaken).ToListAsync();
            var result = new GetNumberOfPeboleInGer3ahDto()
            {
                Number = ger3ahNames.Count()
            };
            return result;

        }

        public async Task<List<GetTheHestoryOfTheGer3ahDto>> GetGer3ahHestory(string name)
        {
            var allUsers = _context.Users.ToList();
            var searcherId = _context.Users.Where(x => x.NameAR == name).FirstOrDefault().Id;
            var ger3ahNames = await _context.Ger3ahLogs.Where(x => x.UserId == searcherId).OrderByDescending(x => x.CreatedDate).ToListAsync();
            var result = new List<GetTheHestoryOfTheGer3ahDto>();

            foreach (var item in ger3ahNames)
            {
                foreach (var item2 in allUsers)
                {
                    if (item.PickedName == item2.Name)
                    {
                        var temp = new GetTheHestoryOfTheGer3ahDto
                        {
                            PickedName = item2.NameAR,
                            PickingDate = item.CreatedDate
                        };
                        result.Add(temp);
                    }
                }
            }
            return result;
        }

        public Ger3ahOutputDto NamePicker(string name, string email)
        {
            try
            {
                var Picker = _context.Users.Where(x => x.NameAR == name).FirstOrDefault();
                if (Picker == null)
                    return new Ger3ahOutputDto { Errors = "the name that was entered  does not exist in the system" };
                if (_context.PickerChrecer.Any(x => x.UserId == Picker.Id && x.IsAlreadyPickedName))
                    return new Ger3ahOutputDto { Errors = " the picer is Already Picked a Name so he cant Picke anther name" };
                var allNames = _context.Ger3ahNames.Include(x => x.User).Where(x => x.UserId != Picker.Id && !x.IsTaken).ToList();
                if (allNames.Count() == 0)
                    return new Ger3ahOutputDto { Errors = "There are no names at the ger3ah" };
                var PickedName = new Ger3ahName();
                var isRandomIswanted = true;
                if (allNames.Count() == 2)
                {
                    var theOnethatNotPicke = _context.PickerChrecer.Where(x => x.UserId != Picker.Id && !x.IsAlreadyPickedName).FirstOrDefault();
                    var theOnethatNotPickeIsStillInTheGer3ah = allNames.Any(x => x.UserId == theOnethatNotPicke.UserId);
                    if (theOnethatNotPickeIsStillInTheGer3ah)
                    {
                        PickedName = allNames.Where(x => x.UserId == theOnethatNotPicke.UserId).FirstOrDefault();
                        isRandomIswanted = false;
                    }
                }

                if (isRandomIswanted)
                {
                    Random R = new Random();
                    PickedName = allNames.ElementAt(R.Next(0, allNames.Count()));
                }

                if (!string.IsNullOrEmpty(email))
                {
                    //if the email has value send a email to it with using Thread
                    Thread sendEmailThread = new Thread(() =>
                            {
                                sendEnailToThePicer(email, PickedName, Picker);
                            });
                    sendEmailThread.Start();


                };

                Ger3ahLog addingThePickerNameAndPickedNameToLog = new Ger3ahLog
                {
                    UserId = Picker.Id,
                    PickerName = Picker.Name,
                    PickedName = PickedName.name,
                    CreatedDate = DateTime.Now
                };

                var Ger3ahNamesNeededToUpdate = _context.Ger3ahNames.Where(x => x.UserId == PickedName.UserId).FirstOrDefault();
                Ger3ahNamesNeededToUpdate.IsTaken = true;

                var PickerChrecerNeededToUpdate = _context.PickerChrecer.Where(x => x.UserId == Picker.Id).FirstOrDefault();
                PickerChrecerNeededToUpdate.IsAlreadyPickedName = true;

                _context.Ger3ahLogs.Add(addingThePickerNameAndPickedNameToLog);
                _context.Ger3ahNames.Update(Ger3ahNamesNeededToUpdate);
                _context.PickerChrecer.Update(PickerChrecerNeededToUpdate);

                _context.SaveChanges();

                return _mapper.Map<Ger3ahName, Ger3ahOutputDto>(PickedName);
            }
            catch (Exception)
            {
                return new Ger3ahOutputDto { Errors = "something went relly wrong" };
            }
        }

        public void ReBuildTheGer3ah()
        {
            var allGer3ahNames = _context.Ger3ahNames.ToList();
            var allPickerChrecer = _context.PickerChrecer.ToList();
            foreach (var item in allGer3ahNames)
            {
                item.IsTaken = false;
                _context.Ger3ahNames.Update(item);
            }
            foreach (var item in allPickerChrecer)
            {
                item.IsAlreadyPickedName = false;
                _context.PickerChrecer.Update(item);
            }
            _context.SaveChanges();
        }

        public RemvedNameStatus RemoveNameFromGer3ah(string name)
        {

            var theRemovedName = _context.Users.Where(x => x.NameAR == name).FirstOrDefault();
            if (theRemovedName == null)
                return new RemvedNameStatus { Status = "the name that was entered  does not exist in the system" };

            var removeFromGer3ahNames = _context.Ger3ahNames.Where(x => x.UserId == theRemovedName.Id && !x.IsTaken).FirstOrDefault();
            var removeFromPickerChrecer = _context.PickerChrecer.Where(x => x.UserId == theRemovedName.Id && !x.IsAlreadyPickedName).FirstOrDefault();

            if (removeFromGer3ahNames == null)
                return new RemvedNameStatus { Status = "Someone picked that name" };

            if (removeFromPickerChrecer == null)
                return new RemvedNameStatus { Status = "that name is Already piced a name" };

            removeFromGer3ahNames.IsTaken = true;
            removeFromPickerChrecer.IsAlreadyPickedName = true;


            _context.Ger3ahNames.Update(removeFromGer3ahNames);
            _context.PickerChrecer.Update(removeFromPickerChrecer);

            _context.SaveChanges();

            return new RemvedNameStatus { Status = "Done" };
        }

        public void sendEnailToThePicer(string to, Ger3ahName PickedName, User Picker)
        {
            string from = "Ger3ah@gmail.com"; //From address    
            // string to = "to@gmail.com"; //To address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = $"السلام عليكم حياك الله <br>  الاسم اللي طلع لك في القرعة ياحبيبنا هو <br> {PickedName.User.NameAR} <br> تمت اضافة خاصية الرسائل لتجعل نسيان الاسم اكثر صعوبة وتحجير في نفس الوقت 😂 <br> ملاحظة: لن يتم ارسال رسالة لك الا في حال انك اجريت القرعة قفط وطلبت ارسال رسالة <br>  فإذا اتتك رسالة خارج هذا الاطار فتجاهلها وعلمني<br> وشكرا لك  ";
            message.Subject = $"{Picker.NameAR} الاسم اللي طلع لك من القرعة هنا !";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("ger3ah@gmail.com", "bmmwhnvogrxjcesh");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception)
            {
            }
        }

    }
}