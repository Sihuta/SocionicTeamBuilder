namespace SocionicTeamBuilder.BLL.DTO
{
    public class IotMeasurementDTO
    {
        public IotMeasurementDTO() { }

        public int Id { get; set; }
        public int TeamMemberId { get; set; }
        public DateTime DateTime { get; set; }
        public byte? HeartBeat { get; set; }
        public double BodyTemperature { get; set; }
        public byte Pulse { get; set; }
        public bool IsFixedOne { get; set; }
        public bool IsCritical { get; set; }
    }
}
