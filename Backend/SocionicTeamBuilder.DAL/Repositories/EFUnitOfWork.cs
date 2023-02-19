using SocionicTeamBuilder.DAL.EF;
using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.DAL.Interfaces;
using Task = SocionicTeamBuilder.DAL.Entities.Task;

namespace SocionicTeamBuilder.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly SocionicTeamBuilderContext dbContext;

        private readonly Lazy<EFGenericRepository<Answer>> lazyAnswerRepository;
        private readonly Lazy<EFGenericRepository<IotMeasurement>> lazyIotMeasurementRepository;
        private readonly Lazy<EFGenericRepository<Dichotomy>> lazyDichotomyRepository;
        private readonly Lazy<EFGenericRepository<Employee>> lazyEmployeeRepository;
        private readonly Lazy<EFGenericRepository<Enemy>> lazyEnemyRepository;
        private readonly Lazy<EFGenericRepository<Enterprise>> lazyEnterpriseRepository;
        private readonly Lazy<EFGenericRepository<Feedback>> lazyFeedbackRepository;
        private readonly Lazy<EFGenericRepository<Question>> lazyQuestionRepository;
        private readonly Lazy<EFGenericRepository<SocionicType>> lazySocionicTypeRepository;
        private readonly Lazy<EFGenericRepository<Task>> lazyTaskRepository;
        private readonly Lazy<EFGenericRepository<Team>> lazyTeamRepository;
        private readonly Lazy<EFGenericRepository<TeamMember>> lazyTeamMemberRepository;
        private readonly Lazy<EFGenericRepository<Testing>> lazyTestingRepository;
        private readonly Lazy<EFGenericRepository<TestingResult>> lazyTestingResultRepository;
        private readonly Lazy<EFGenericRepository<User>> lazyUserRepository;

        private bool disposedValue = false;

        public EFUnitOfWork(SocionicTeamBuilderContext context)
        {
            dbContext = context;

            lazyAnswerRepository = new Lazy<EFGenericRepository<Answer>>(
                () => new EFGenericRepository<Answer>(dbContext));
            lazyIotMeasurementRepository = new Lazy<EFGenericRepository<IotMeasurement>>(
                () => new EFGenericRepository<IotMeasurement>(dbContext));
            lazyDichotomyRepository = new Lazy<EFGenericRepository<Dichotomy>>(
                () => new EFGenericRepository<Dichotomy>(dbContext));
            lazyEmployeeRepository = new Lazy<EFGenericRepository<Employee>>(
                () => new EFGenericRepository<Employee>(dbContext));
            lazyEnemyRepository = new Lazy<EFGenericRepository<Enemy>>(
                () => new EFGenericRepository<Enemy>(dbContext));
            lazyEnterpriseRepository = new Lazy<EFGenericRepository<Enterprise>>(
                () => new EFGenericRepository<Enterprise>(dbContext));
            lazyFeedbackRepository = new Lazy<EFGenericRepository<Feedback>>(
                () => new EFGenericRepository<Feedback>(dbContext));
            lazyTestingRepository = new Lazy<EFGenericRepository<Testing>>(
                () => new EFGenericRepository<Testing>(dbContext));
            lazyTestingResultRepository = new Lazy<EFGenericRepository<TestingResult>>(
                () => new EFGenericRepository<TestingResult>(dbContext));
            lazyQuestionRepository = new Lazy<EFGenericRepository<Question>>(
                () => new EFGenericRepository<Question>(dbContext));
            lazySocionicTypeRepository = new Lazy<EFGenericRepository<SocionicType>>(
                () => new EFGenericRepository<SocionicType>(dbContext));
            lazyTaskRepository = new Lazy<EFGenericRepository<Task>>(
                () => new EFGenericRepository<Task>(dbContext));
            lazyTeamRepository = new Lazy<EFGenericRepository<Team>>(
                () => new EFGenericRepository<Team>(dbContext));
            lazyTeamMemberRepository = new Lazy<EFGenericRepository<TeamMember>>(
                () => new EFGenericRepository<TeamMember>(dbContext));
            lazyUserRepository = new Lazy<EFGenericRepository<User>>(
                () => new EFGenericRepository<User>(dbContext));
        }

        public IGenericRepository<Answer> AnswerRepository => lazyAnswerRepository.Value;
        public IGenericRepository<IotMeasurement> IotMeasurementRepository => lazyIotMeasurementRepository.Value;
        public IGenericRepository<Dichotomy> DichotomyRepository => lazyDichotomyRepository.Value;
        public IGenericRepository<Employee> EmployeeRepository => lazyEmployeeRepository.Value;
        public IGenericRepository<Enterprise> EnterpriseRepository => lazyEnterpriseRepository.Value;
        public IGenericRepository<Question> QuestionRepository => lazyQuestionRepository.Value;
        public IGenericRepository<SocionicType> SocionicTypeRepository => lazySocionicTypeRepository.Value;
        public IGenericRepository<Task> TaskRepository => lazyTaskRepository.Value;
        public IGenericRepository<Team> TeamRepository => lazyTeamRepository.Value;
        public IGenericRepository<TeamMember> TeamMemberRepository => lazyTeamMemberRepository.Value;
        public IGenericRepository<Testing> TestingRepository => lazyTestingRepository.Value;
        public IGenericRepository<TestingResult> TestingResultRepository => lazyTestingResultRepository.Value;
        public IGenericRepository<User> UserRepository => lazyUserRepository.Value;
        public IGenericRepository<Enemy> EnemyRepository => lazyEnemyRepository.Value;
        public IGenericRepository<Feedback> FeedbackRepository => lazyFeedbackRepository.Value;

        public async System.Threading.Tasks.Task CommitAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
