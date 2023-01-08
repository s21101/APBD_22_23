namespace Cw9.DTO.Responce
{
    public class DoctorDTO
    {
        public int IdDoctor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<PrescriptionDTO> Prescriptions { get; set; }
    }
}
