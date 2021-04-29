namespace StudentTutor.Core.Models.Interfaces
{
    public interface ISubjectOfInterestModel
    {
        int Id { get; set; }
        string Note { get; set; }
        string SubjectTopics { get; set; }
    }
}