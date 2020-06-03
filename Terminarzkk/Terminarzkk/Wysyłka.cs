using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Plugin.Messaging;

namespace Terminarzkk
{
    public class Wysylka
    {
        private Email k = new Email();
        private Dbemail h = new Dbemail();
        bool fa = true;
        public void Wysla_wiadomosc(Wydarzenie nowe)
        {
            var emailmes = CrossMessaging.Current.EmailMessenger;
            if (emailmes.CanSendEmail)
            {
                try {
                    k = h.GetEmail();
                }
                catch (Exception ex) { fa = false; }
                if (fa) {

                    try
                    {

                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                        mail.From = new MailAddress(k.Nazwa);
                        mail.To.Add(k.Nazwa);
                        mail.Subject = "Nadchodzi wydarzenie " + nowe.Nazwa + " Dnia " + nowe.Dzien;
                        mail.Body = "Dnia " + nowe.Dzien + "-" + nowe.Miesiac + "-" + nowe.Rok + " odbędzie się wydarzenie " + nowe.Nazwa + " w lokalizacji " + nowe.Gdzie + " o godzinie " + nowe.Godz + " Informacje szczególne: " + nowe.Uwagi + "/n     Miłego spotkania!!!";
                        SmtpServer.Port = 587;
                        SmtpServer.Host = "smtp.gmail.com";
                        SmtpServer.EnableSsl = true;
                        SmtpServer.UseDefaultCredentials = false;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(k.Nazwa, k.Password);

                        SmtpServer.Send(mail);
                    }
                    catch (Exception ex)
                    {
                    }
                    nowe.Wyslano = 1;
                    Dbwydarzenie bdw = new Dbwydarzenie();
                    bdw.ZapiszWydarzenie(nowe);
                }
            }
        }
        public void Czywyslc()
        {
            int dzienwyslania = DateTime.Today.AddDays(1).Day;
            int miesiacwyslania = DateTime.Today.Month;
            int rokwyslnia = DateTime.Today.Year;
            Dbwydarzenie baza = new Dbwydarzenie();
            List<Wydarzenie> wys = new List<Wydarzenie>();
            wys.AddRange(baza.GetKazdeWydarzenianiewys(miesiacwyslania, rokwyslnia, dzienwyslania));
            for (int i = 0; i < wys.Count; i++)
            {
                Wysla_wiadomosc(wys[i]);
            }



        }
        }
}