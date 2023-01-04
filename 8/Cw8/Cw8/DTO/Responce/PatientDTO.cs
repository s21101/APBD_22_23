namespace Cw8.DTO.Responce
{
    public class PatientDTO
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public IEnumerable<PrescriptionDTO> Prescriptions { get; set; }
    }
}