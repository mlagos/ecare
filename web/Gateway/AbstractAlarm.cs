using Nextgal.ECare.Model.Position.Dto;

namespace Nextgal.ECare.Gateway
{
    public abstract class AbstractAlarm
    {
        public abstract void CheckAlarm(PositionDto positionDto);
        public abstract bool isActiveAlarm(PositionDto positionDto);
        public abstract void HandleAlarm(PositionDto positionDto);
        public abstract void SaveAlarm(PositionDto positionDto);
    }
}