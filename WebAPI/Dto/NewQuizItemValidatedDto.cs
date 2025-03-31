namespace WebAPI.Dto;

public class NewQuizItemValidatedDto
{
    public string Question { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new List<string>();
    public int CorrectOptionIndex { get; set; }
}