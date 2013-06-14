using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Timers;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Event.Dto;
using Nextgal.ECare.Model.Event.Facade;
using Nextgal.ECare.Model.Message.Dto;
using Nextgal.ECare.Model.Message.Facade;
using PushSharp;
using PushSharp.Apple;
using Timer = System.Timers.Timer;


namespace Nextgal.ECare.WebTeleasistencia.App_Code.MessageManager
{
    public class Gsm
    {
        private Timer eventCheck = new Timer(120000);

        public void StartTimer()
        {
            eventCheck.Enabled = true;
            eventCheck.Elapsed += new ElapsedEventHandler(CompruebaEventos);
        }

        private void CompruebaEventos(Object obj, ElapsedEventArgs e)
        {
            ArrayList eventos = EventFacade.GetInstance().GetAllNoSentEvents();

            foreach (var evento in eventos)
            {
                CustomEventDto eventDto = (CustomEventDto)evento;
                CustomMessageDto dto = new CustomMessageDto(1, -1, eventDto.IdActive, DateTime.MinValue, DateTime.MinValue, eventDto.Text, eventDto.Phone);
                EnviarEvento(dto, eventDto);
            }
        }

        public void StopTimer()
        {
            eventCheck.Enabled = false;
        }

        private void EnviarEvento(CustomMessageDto dto, CustomEventDto cEventDto)
        {
        
            EventDto eventDto = new EventDto(cEventDto.IdEvent,cEventDto.IdActive, cEventDto.Date, cEventDto.Text, DateTime.Now);
            EventFacade.GetInstance().Update(eventDto);
            dto.Text = dto.Text + "#";
            Send_PushMessage(dto.IdActive, dto.Text, "EVT");
        }

        public void EnviarEmergencia(CustomMessageDto dto)
        {
            dto.Text = dto.Text + "#";
            Send_PushMessage(dto.IdActive, dto.Text, "SOS");
        }

        public void EnviarConfiguracion(CustomMessageDto dto)
        {
            dto.Text = dto.Text + "#";
            Send_PushMessage(dto.IdActive, dto.Text, "CFG");
        }

        public void EnviarMensaje(CustomMessageDto dto)
        {
            MessageDto MessDto = MessageFacade.GetInstance().CreateMessage(new MessageDto(dto.IdMessage, dto.IdUser, dto.IdActive, DateTime.Now, DateTime.MinValue, dto.Text));
            dto.Text=MessDto.IdMessage+";"+dto.Text+"#";
            Send_PushMessage(dto.IdActive, dto.Text, "MSG");
        }

        protected void Send_PushMessage(int idActive, string text, string tipo)
        {
            AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdActive(idActive);
            
            //Configure and start Apple APNS
            var appleCert =
                File.ReadAllBytes(@"D:\WWW\familyecare.com\web\Resources\es.nextgal.familyecare.push.develop.p12");
            PushService push = new PushService();
            push.StartApplePushService(new ApplePushChannelSettings(false, appleCert, "n3xtg4l"));

            push.QueueNotification(NotificationFactory.Apple()
                .ForDeviceToken(assistedDto.DeviceToken)
                .WithCustomItem(tipo, text.Trim()));
        }


        private string ClearMessage(string s)
        {
            s = s.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n");
            return s.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("Ñ", "N");
        }

        private Gsm()
        {

        }

        private static Gsm instance;

        public static Gsm GetInstance()
        {
            if (instance == null)
            {
                instance = new Gsm();
            }
            return instance;
        }
    }
}
