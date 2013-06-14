using System;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;

namespace Nextgal.ECare.Gateway
{
    public class DeviceFactory
    {
        public static AbstractDevice GetInstance(string msg, byte[] dataReceived)
        {
            char[] delim = {'<', ' ', ',','&',';'};
            string identifier = msg.StartsWith("<") ? msg.Split(delim)[1] : msg.Split(delim)[0];
            try
            {
                AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdentifier(identifier);
                //return new Symbian(assistedDto.Identifier);
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
