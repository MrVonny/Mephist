using Mephist.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Services
{
    public class UniversityStaticData
    {
        private readonly UniversityContext _context;

        private List<string> subjects;
        private List<(int,string)> laboratorySubjects;
        private Dictionary<string, List<string>> laboratoryWorks;

        public UniversityStaticData(UniversityContext context)
        {
            _context = context;
            subjects = new List<string>();
            foreach (var list in _context.Employees.Select(em => em.Subjects))
                if (list != null) subjects.AddRange(list);
            subjects = subjects.Distinct().ToList();

            laboratorySubjects = new List<(int, string)>()
            {
                (1,"Общая физика (механика)"),
                (2,"Общая физика (молекулярная физика и основы статистической термодинамики)"),
                (3,"Общая физика (электричество и магнетизм)"),
                (4,"Общая физика (волны и оптика)")
            };

            laboratoryWorks = new Dictionary<string, List<string>>()
            {
                { laboratorySubjects[0].Item2, new List<string>(){
                    "Работа 1.1. Измерение массы, длины и времени",
                    "Работа 1.1а. Изучение амперметра и вольтметра",
                    "Работа 1.2. Изучение катетометра и сферометра",
                    "Работа 1.3. Изучение свободного падения тел",
                    "Работа 1.4. Изучение второго закона Ньютона с использованием воздушной дорожки",
                    "Работа 1.5. Изучение законов сохранения импульса и энергии при упругом и неупругом столкновениях",
                    "Работа 1.6. Измерение скорости полета пули методом вращающихся дисков ",
                    "Работа 1.7. Модуль упругости",
                    "Работа 1.8. Модуль сдвига и механический гистерезис",
                    "Работа 1.9. Определение гравитационной постоянной",
                    "Работа 1.10. Изучение сил инерции. Центробежная сила",
                    "Работа 1.11 (1.11а, 1.11б). Определение вязкости жидкости",
                    "Работа 1.12. Измерение времени соударения шаров",
                    "Работа 1.12а. Измерение времени соударения стержней и определение модуля Юнга вещества",
                    "Работа 1.13. Исследование кинематики распада релятивистских частиц ",
                    "Работа 1.14. Изучение динамики движения заряженных частиц в электрическом и магнитном полях с помощью электронно-лучевой трубки",
                    "Работа 1.15. Исследование собственных колебаний струны методом резонанса",
                    "Работа 1.16(1.16а). Определение ускорения свободного падения с помощью оборотного маятника",
                    "Работа 1.17(1.17а). Изучение динамики вращательного движения физических тел",
                    "Работа 1.17б. Изучение динамики вращательного движения физических тел",
                    "Работа 1.18(1.18а, 1.18б). Определение моментов инерции тел методом крутильных колебаний",
                    "Работа 1.19. Определение эллипсоида инерции твердого тела методом крутильных колебаний",
                    "Работа 1.20. Изучение динамики поступательного движения тел с помощью машины Атвуда",
                    "Работа 1.21(1.21а). Изучение динамики плоского движения физических тел",
                    "Работа 1.22(1.22а). Изучение гироскопа",
                    "Работа 1.23. Экспериментальное определение коэффициента трения качения с помощью наклонного маятника",
                    "Работа 1.24. Определение скорости пули с помощью баллистического маятника"

                }},

                { laboratorySubjects[1].Item2, new List<string>(){

                }},

                { laboratorySubjects[2].Item2, new List<string>(){

                }},

                { laboratorySubjects[3].Item2, new List<string>(){

                }},
            };
        }

        public IEnumerable<string> GetSubjects()
        {
            return subjects;
        }

        public IEnumerable<string> GetLaboratorySubjects()
        {
            return laboratorySubjects.Select(s => s.Item2);
        }

        public IEnumerable<(int,string)> GetLaboratorySubjectsWithSemestr()
        {
            return laboratorySubjects;
        }

        public IEnumerable<string> GetLaboratoryWorks(string subject)
        {
            if (!laboratoryWorks.ContainsKey(subject))
                throw new InvalidCastException();
            return laboratoryWorks[subject];
        }
        public int GetSemestrBySubject(string subject)
        {
                return GetLaboratorySubjectsWithSemestr().Single(x => x.Item2.Equals(subject)).Item1;
        }


    }
}
