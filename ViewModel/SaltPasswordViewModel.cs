namespace OneStopApp_Api.ViewModel
{
    public class SaltPasswordViewModel
    {
        public int SaltBytes { get; set; }

        public int HashBytes { get; set; }

        public int Iterations { get; set; }

        public int IterationIndex { get; set; }

        public int SaltIndex { get; set; }

        public int Index { get; set; }
    }

}