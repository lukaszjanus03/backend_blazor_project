namespace WebAPI.Dto;

public class NewQuizItemDto
{
    public string Question { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public List<string> IncorrectAnswers { get; set; } = new List<string>();

}