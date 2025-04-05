using MiniPerson.Domain.Common;

namespace MiniPerson.Domain.Entities
{
    public class Person : BaseEntity
    {
        #region Props
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string NationCode { get; private set; }
        public string BirthDate { get; private set; }
        #endregion

        #region Ctor
        private Person() { }

        public Person(string firstName,
                      string lastName,
                      string nationCode,
                      string birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            NationCode = nationCode;
            BirthDate = birthDate;

        }

        public Person(long id,
                      string firstName,
                      string lastName,
                      string nationCode,
                      string birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            NationCode = nationCode;
            BirthDate = birthDate;
        }
        #endregion

        #region Commands
        public static Person Create(string firstName,
                                    string lastName,
                                    string nationCode,
                                    string birthDate)
            => new(firstName, lastName, nationCode, birthDate);

        public void Update(Person person)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            NationCode = person.NationCode;
            BirthDate = person.BirthDate;
        }

        public static Person UpdateNew(long id,
                                    string firstName,
                                    string lastName,
                                    string nationCode,
                                    string birthDate)
        {
            return new Person(id, firstName, lastName, nationCode, birthDate);
        }
        #endregion
    }
}