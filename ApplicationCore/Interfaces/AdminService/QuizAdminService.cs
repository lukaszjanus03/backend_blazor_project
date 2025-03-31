using ApplicationCore.Interfaces.Repository;

namespace BackendLab01;

public class QuizAdminService:IQuizAdminService
{
    private IGenericRepository<Quiz, int> _quizRepository;
    private IGenericRepository<QuizItem, int> _itemRepository;

    public QuizAdminService(IGenericRepository<Quiz, int> quizRepository, IGenericRepository<QuizItem, int> itemRepository)
    {
        this._quizRepository = quizRepository;
        this._itemRepository = itemRepository;
    }
    
    public Quiz AddQuiz(Quiz quiz)
    {
        return _quizRepository.Add(quiz);
    }

    public bool DeleteQuiz(int id)
    {

        if (_quizRepository.FindById(id).Items.Count == 0)
        {
            _quizRepository.RemoveById(id);
            return true;
        }
            
        return false;
    }

    public QuizItem AddQuizItemToQuiz(int quizId, QuizItem item)
    {
        var quiz = _quizRepository.FindById(quizId);
        if (quiz is null)
        {
            throw new Exception();
        }
        var newItem = _itemRepository.Add(item);
        quiz.Items.Add(newItem);
        _quizRepository.Update(quizId, quiz);
        return newItem;
    }

    public QuizItem AddQuizItem(string question, List<string> incorrectAnswers, string correctAnswer, int points)
    {
        return _itemRepository.Add(new QuizItem(question: question, incorrectAnswers: incorrectAnswers, correctAnswer: correctAnswer, id: 0));
    }

    public void UpdateQuizItem(int id, string question, List<string> incorrectAnswers, string correctAnswer, int points)
    {
        var quizItem = new QuizItem(id: id, question: question, incorrectAnswers: incorrectAnswers, correctAnswer: correctAnswer);
        _itemRepository.Update(id, quizItem);
    }

    public Quiz AddQuiz(string title, List<QuizItem> items)
    {
        return _quizRepository.Add(new Quiz( 0, title: title, items: items));
    }

    public List<QuizItem> FindAllQuizItems()
    {
        return _itemRepository.FindAll();
    }

    public List<Quiz> FindAllQuizzes()
    { return _quizRepository.FindAll();
    }
    public Quiz UpdateQuiz(int quizId, Quiz updatedQuiz)
    {
        var existingQuiz = _quizRepository.FindById(quizId);
        if (existingQuiz == null)
        {
            throw new Exception($"Quiz with ID {quizId} not found.");
        }
        existingQuiz.Title = updatedQuiz.Title;
        existingQuiz.Items = updatedQuiz.Items;
        
        _quizRepository.Update(quizId, existingQuiz);
    
        return existingQuiz;
    }
}