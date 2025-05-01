namespace middleware.DataBase
{
    public class EmeterLog
    {
        public int Id { get; set; }
        public int MeterID { get; set; }
        public double? U1 { get; set; }
        public double? U2 { get; set; }
        public double? U3 { get; set; }
        public double? ULN { get; set; }
        public double? ULL { get; set; }
        public double? I1 { get; set; }
        public double? I2 { get; set; }
        public double? I3 { get; set; }
        public double? IL { get; set; }
        public double? P1 { get; set; }
        public double? P2 { get; set; }
        public double? P3 { get; set; }
        public double? P { get; set; }
        public double? Q { get; set; }
        public double? S { get; set; }
        public double? PF { get; set; }
        public double? F { get; set; }
        public double? Ph { get; set; }
        public double? Qh { get; set; }
        public double? Ah { get; set; }

        public int Time { get; set; }
    }
}
