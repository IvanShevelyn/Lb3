using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lb3
{
    public class Factory : IComparable<Factory>
    {
        #region Реализация интерфейсов Icomparable<> и IComparer<>
        public class SortFactoryByMasters : IComparer<Factory>
        {
            int IComparer<Factory>.Compare(Factory x, Factory y)
            {
                if (x.CountOfMasters > y.CountOfMasters)
                    return 1;
                if (x.CountOfMasters < y.CountOfMasters)
                    return -1;
                else
                    return 0;
            }
        }
        public class SortFactoryByEmployees : IComparer<Factory>
        {
            int IComparer<Factory>.Compare(Factory x, Factory y)
            {
                if (x.CountOfEmployees > y.CountOfEmployees)
                    return 1;
                if (x.CountOfEmployees < y.CountOfEmployees)
                    return -1;
                else
                    return 0;
            }
        }
        public class SortFactoryByDepartments : IComparer<Factory>
        {
            int IComparer<Factory>.Compare(Factory x, Factory y)
            {
                if (x._workshops.Count() > y._workshops.Count())
                    return 1;
                if (x._workshops.Count() < y._workshops.Count())
                    return -1;
                else
                    return 0;
            }
        }
        public int CompareTo(Factory other) //базовая сортировка
        {
            return string.Compare(this.FactoryName, other.FactoryName);
        }
        public static IComparer<Factory> SortFactoryByMastersHelper()
        {
            return (IComparer<Factory>) new SortFactoryByMasters();
        }
        public static IComparer<Factory> SortFactoryByEmployeesHelper()
        {
            return (IComparer<Factory>) new SortFactoryByEmployees();
        }
        public static IComparer<Factory> SortFactoryByDepartmentsHelper()
        {
            return (IComparer<Factory>) new SortFactoryByDepartments();
        }
        #endregion
        public Dictionary<Workshop, List<Person>> dictionary = new Dictionary<Workshop, List<Person>>();//цех - ключ
        private List<Workshop> _workshops = new List<Workshop>();
        private Dictionary<string, Person> _listOfWorkers = new Dictionary<string, Person>();//податковий номер - ключ
        public int CountOfCreatedGetails { get; set; }
        public string FactoryName { get => _FactoryName; }
        private string _FactoryName;
        public int CountOfDepartment 
        {
            get
            {
                _CountOfDepartment = _workshops.Count();
                return _CountOfDepartment;
            }
        }
        private int _CountOfDepartment;
        public int CountOfMasters
        {
            get
            {
                int count = 0;
                foreach (var item in _listOfWorkers)
                {
                    if(item.Value is Master)
                    {
                        count++;
                    }
                }
                _CountOfMasters = count;
                return _CountOfMasters;
            }
        }
        private int _CountOfMasters;
        public int CountOfEmployees 
        {
            get
            {
                int count = 0;
                foreach (var item in _listOfWorkers)
                {
                    if (item.Value is Worker)
                    {
                        count++;
                    }
                }
                _CountOfEmployees = count;
                return _CountOfEmployees;
            }
        }
        private int _CountOfEmployees;
        public int MasterSalary { get => _MasterSalary; }
        private int _MasterSalary;
        public int EmployeeSalary { get => _EmployeeSalary; }
        private int _EmployeeSalary;
        public int MonthBenefitFromMaster { get => _MonthBenefitFactoryFromMaster; }
        private int _MonthBenefitFactoryFromMaster;
        public int MonthBenefitFromEmployee { get => _MonthBenefitFactoryFromEmployee; }
        private int _MonthBenefitFactoryFromEmployee;

        public Factory() { }
        public Factory(string FactoryName, IEnumerable<Workshop> workshops, IEnumerable<Person> persons, int MasterSalary, int EmployeeSalary, int MonthBenefitFactoryFromEmployee, int MonthBenefitFactoryFromMaster, int NumberOfCreatedDetails, int NumberOfMasters, int NumberOfEmployees, int NumberOfDepart)
            :this(MasterSalary, EmployeeSalary, MonthBenefitFactoryFromEmployee, MonthBenefitFactoryFromMaster, NumberOfCreatedDetails, NumberOfMasters, NumberOfEmployees, NumberOfDepart)
        {
            _FactoryName = FactoryName;
            foreach (Person pers in persons)
            {
                _listOfWorkers.Add(pers.IndividualTaxNumber, pers);
            }
            foreach (Workshop work in workshops)
            {
                _workshops.Add(work);
                List<Person> Persons = new List<Person>();
                foreach (Person pers in persons)
                {  
                    if (pers.ID_Workshop != null)
                    {
                        if (pers.ID_Workshop.ToString() == work.ID.ToString())
                            Persons.Add(pers);
                    }
                }
                dictionary.Add(work, Persons);
            }
        }
        public Factory(string FactoryName, int MasterSalary, int EmployeeSalary, int MonthBenefitFactoryFromEmployee, int MonthBenefitFactoryFromMaster) // конструктор, используемый при создании заводов в приложении 
        {
            _FactoryName = FactoryName;
            _CountOfMasters = 0;
            _CountOfEmployees = 0;
            _MasterSalary = MasterSalary;
            _EmployeeSalary = EmployeeSalary;
            _MonthBenefitFactoryFromEmployee = MonthBenefitFactoryFromEmployee;
            _MonthBenefitFactoryFromMaster = MonthBenefitFactoryFromMaster;
        }
        public Factory(int MasterSalary, int EmployeeSalary, int MonthBenefitFactoryFromEmployee, int MonthBenefitFactoryFromMaster, int NumberOfCreatedDetails, int NumberOfMasters, int NumberOfEmployees, int NumberOfDepart) // конструктор используемый для инициализации, вызывается когда вызывается конструктор со списками
        {
            _CountOfMasters = NumberOfMasters;
            _CountOfEmployees = NumberOfEmployees;
            CountOfCreatedGetails = NumberOfCreatedDetails;
            _CountOfDepartment = NumberOfDepart;
            _MasterSalary = MasterSalary;
            _EmployeeSalary = EmployeeSalary;
            _MonthBenefitFactoryFromEmployee = MonthBenefitFactoryFromEmployee;
            _MonthBenefitFactoryFromMaster = MonthBenefitFactoryFromMaster;
        }

        public Factory(Factory other) //конструктор копирования  
        {
            foreach (Workshop item in other._workshops)
            {
                Workshop newWrk = (Workshop)item.Clone();
                _workshops.Add(newWrk);
            }
            foreach (var item in other._listOfWorkers)
            {
                Person newPerson = (Person)item.Value.Clone();
                _listOfWorkers.Add(newPerson.IndividualTaxNumber, newPerson);
            }
            foreach (Workshop work in _workshops)
            {
                List<Person> Persons = new List<Person>();
                foreach (var pers in _listOfWorkers)
                {
                    if (pers.Value.ID_Workshop != null)
                    {
                        if (pers.Value.ID_Workshop.ToString() == work.ID.ToString())
                            Persons.Add(pers.Value);
                    }
                }
                dictionary.Add(work, Persons);
            }
            CountOfCreatedGetails = other.CountOfCreatedGetails;
            _FactoryName = other._FactoryName + "(copy)";
            _CountOfDepartment = other._CountOfDepartment;
            _CountOfMasters = other._CountOfMasters;
            _CountOfEmployees = other._CountOfEmployees;
            _MasterSalary = other._MasterSalary;
            _EmployeeSalary = other._EmployeeSalary;
            _MonthBenefitFactoryFromMaster = other._MonthBenefitFactoryFromMaster;
            _MonthBenefitFactoryFromEmployee = other._MonthBenefitFactoryFromEmployee;
        }

        public void HireEmployee(Person person) //метод нанятия работника
        {
            if (_workshops.Count == 0)
            {
                MessageBox.Show("Немає майстрів! Неможливо найняти робітника");
                return;
            }
            int index = 0;
            for (int i = 0; i < _workshops.Count(); i++)
            {
                if (_workshops[i].CountOfEmployees != Workshop.MaxCapacityOfEmployees)
                {
                    index = i;
                    break;
                }
            }
            if (_workshops[index].CountOfMasters == 0)
            {
                MessageBox.Show("Немає майстрів! Неможливо найняти робітника");
                return;
            }
            if (_workshops[index].CountOfEmployees / _workshops[index].CountOfMasters == 10 && _workshops[index].CountOfEmployees % 10 == 0)
            {
                MessageBox.Show("Забагато працівників, треба найняти майстра");
                return;
            }
            else
            {
                _workshops[index].CountOfWorkPlaces--;
                _workshops[index].CountOfEmployees++;
                person.ID_Workshop = _workshops[index].ID.ToString();
                _listOfWorkers.Add(person.IndividualTaxNumber, person);
                dictionary[_workshops[index]].Add(person);
            }
        }
        public void HireMaster(Person person) //метод нанятия мастера
        {
            bool flag = false;
            int index = 0;
            for (int i = 0; i < _workshops.Count(); i++)
            {
                if (_workshops[i].CountOfMasters != Workshop.MaxCapacityOfMasters || _workshops[i].CountOfEmployees != Workshop.MaxCapacityOfEmployees)
                {
                    flag = true;
                    index = i;
                    break;
                }
            }
            if (_workshops.Count() == 0)
            {
                _workshops.Add(new Workshop());
                dictionary.Add(_workshops[0], new List<Person>());
                MessageBox.Show("Створено новий цех!");
            }
            else if (flag == false)
            {
                _workshops.Add(new Workshop());
                MessageBox.Show("Створено новий цех!");
                index = _workshops.Count() - 1;
                dictionary.Add(_workshops[index], new List<Person>());
            }
            if (_workshops[index].CountOfMasters == 0)
            {
                _workshops[index].CountOfWorkPlaces--;
                _workshops[index].CountOfMasters++;
                person.ID_Workshop = _workshops[index].ID.ToString();
                _listOfWorkers.Add(person.IndividualTaxNumber, person);
                dictionary[_workshops[index]].Add(person);
                MessageBox.Show("Майстра найнято, тепер наймайте працівників!");
                return;
            }
            if (_workshops[index].CountOfEmployees % 10 == 0 && _workshops[index].CountOfEmployees / _workshops[index].CountOfMasters == 10)
            {
                _workshops[index].CountOfWorkPlaces--;
                _workshops[index].CountOfMasters++;
                person.ID_Workshop = _workshops[index].ID.ToString();
                _listOfWorkers.Add(person.IndividualTaxNumber, person);
                dictionary[_workshops[index]].Add(person);
                MessageBox.Show("Майстра найнято, тепер наймайте працівників!");
                return;
            }
            else
            {
                MessageBox.Show("Замало працівників для того, щоб найняти майстра");
            }
        }
        //////  
        public void DismissEmployee(string TaxNumber) //метод увольнения работника
        {
            if (_listOfWorkers.TryGetValue(TaxNumber, out Person pers))
            {
                foreach (Workshop workshop in _workshops)
                {
                    if (workshop.ID.ToString() == pers.ID_Workshop)
                    {
                        if (pers is Worker)
                        {
                            workshop.CountOfWorkPlaces++;
                            workshop.CountOfEmployees--;
                            dictionary[workshop].Remove(pers);
                            _listOfWorkers.Remove(TaxNumber);
                            if (workshop.CountOfWorkPlaces == Workshop.MaxCapacity)
                            {
                                _workshops.Remove(workshop);
                                dictionary.Remove(workshop);
                                MessageBox.Show($"Робітника із податковим номером {TaxNumber} було звільнено!");
                                MessageBox.Show("Цех було видалено, бо у ньому більше ніхто не працює!");
                                break;
                            }
                            bool flag = false;
                            if (workshop.CountOfMasters * 10 - workshop.CountOfEmployees >= 10)
                            {
                                flag = true;
                                workshop.CountOfWorkPlaces++;
                                workshop.CountOfMasters--;
                                foreach (var ms in _listOfWorkers)
                                {
                                    if (ms.Value is Master && ms.Value.ID_Workshop == workshop.ID.ToString())
                                    {
                                        _listOfWorkers.Remove(ms.Key);
                                        dictionary[workshop].Remove(ms.Value);
                                        break;
                                    }
                                }
                            }
                            if (flag)
                                MessageBox.Show("Було звільнено працівника та майстра, бо забагато майстрів!");
                            else
                                MessageBox.Show($"Робітника із податковим номером {TaxNumber} було звільнено!");
                            if (workshop.CountOfWorkPlaces == Workshop.MaxCapacity)
                            {
                                _workshops.Remove(workshop);
                                dictionary.Remove(workshop);
                                MessageBox.Show("Цех було видалено, бо у ньому більше ніхто не працює!");
                            }
                            break;
                        }
                        else
                        {
                            workshop.CountOfWorkPlaces++;
                            workshop.CountOfMasters--;
                            dictionary[workshop].Remove(pers);
                            _listOfWorkers.Remove(TaxNumber);
                            if (workshop.CountOfWorkPlaces == Workshop.MaxCapacity)
                            {
                                _workshops.Remove(workshop);
                                dictionary.Remove(workshop);
                                MessageBox.Show($"Майстра із податковим номером {TaxNumber} було звільнено!");
                                MessageBox.Show("Цех було видалено, бо у ньому більше ніхто не працює!");
                                break;
                            }
                            bool flag = false;
                            while (workshop.CountOfEmployees - workshop.CountOfMasters * 10 != 0)
                            {
                                flag = true;
                                workshop.CountOfWorkPlaces++;
                                workshop.CountOfEmployees--;
                                foreach (var wrk in _listOfWorkers)
                                {
                                    if (wrk.Value is Worker && wrk.Value.ID_Workshop == workshop.ID.ToString())
                                    {
                                        _listOfWorkers.Remove(wrk.Key);
                                        dictionary[workshop].Remove(wrk.Value);
                                        break;
                                    }
                                }
                            }
                            if (flag)
                                MessageBox.Show("Майстра звільнено!\rВідбулося скорочення працівників, бо на заводі тепер працює замало майстрів!");
                            else
                                MessageBox.Show($"Майстра із податковим номером {TaxNumber} було звільнено!");
                            if (workshop.CountOfWorkPlaces == Workshop.MaxCapacity)
                            {
                                _workshops.Remove(workshop);
                                dictionary.Remove(workshop);
                                MessageBox.Show("Цех було видалено, бо у ньому більше ніхто не працює!");
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
