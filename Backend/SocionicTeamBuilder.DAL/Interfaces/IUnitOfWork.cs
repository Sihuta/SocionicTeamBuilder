using SocionicTeamBuilder.DAL.Entities;
using Task = SocionicTeamBuilder.DAL.Entities.Task;

namespace SocionicTeamBuilder.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Answer> AnswerRepository { get; }
        IGenericRepository<IotMeasurement> IotMeasurementRepository { get; }
        IGenericRepository<Dichotomy> DichotomyRepository { get; }
        IGenericRepository<Employee> EmployeeRepository { get; }
        IGenericRepository<Enemy> EnemyRepository { get; }
        IGenericRepository<Enterprise> EnterpriseRepository { get; }
        IGenericRepository<Feedback> FeedbackRepository { get; }
        IGenericRepository<Question> QuestionRepository { get; }
        IGenericRepository<SocionicType> SocionicTypeRepository { get; }
        IGenericRepository<Task> TaskRepository { get; }
        IGenericRepository<Team> TeamRepository { get; }
        IGenericRepository<TeamMember> TeamMemberRepository { get; }
        IGenericRepository<Testing> TestingRepository { get; }
        IGenericRepository<TestingResult> TestingResultRepository { get; }
        IGenericRepository<User> UserRepository { get; }


        System.Threading.Tasks.Task CommitAsync();
    }
}
