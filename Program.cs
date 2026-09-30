using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqGyakorlo
{
    class Program
    {
        static void Main(string[] args)
        {
            // A feladatok leírását a Feladatlap.md fájlban találod.
            // Minden feladathoz tartozik egy Feladat##() metódus itt lent.
            // Írd meg a LINQ lekérdezést a metódus törzsében, majd
            // vedd ki a kommentet a hívása elől, hogy lásd az eredményt.

            Feladat01();
            Feladat02();
            Feladat03();
            Feladat04();
            Feladat05();
            Feladat06();
            Feladat07();
            Feladat08();
            Feladat09();
            Feladat10();
            Feladat11();
            Feladat12();
            Feladat13();
            Feladat14();
            Feladat15();
            Feladat16();
            Feladat17();
             Feladat18();
             Feladat19();
             Feladat20();
             Feladat21();
             Feladat22();
             Feladat23();
             Feladat24();
             Feladat25();
             Feladat26();
             Feladat27();
             Feladat28();
             Feladat29();
             Feladat30();
             Feladat31();
             Feladat32();
             Feladat33();
             Feladat34();
             Feladat35();
             Feladat36();
             Feladat37();
             Feladat38();
             Feladat39();
             Feladat40();
        }

        // ---------- 1. Szűrés — Where ----------

        // 1. Hallgatók, akiknek 4.0 fölötti az átlaga.
        static void Feladat01()
        {
            var result = SampleData.Students.Where(atlag => atlag.GradeAverage > 4);
            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
        }


        // 2. Budapesti hallgatók.
        static void Feladat02()
        {
            var result = SampleData.Students.Where(varos => varos.City == "Budapest");
            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
        }

        // 3. Kurzusok, amelyek kreditértéke legalább 5.
        static void Feladat03()
        {
            var result = SampleData.Courses.Where(credits => credits.Credit > 5);
            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
        }

        // 4. Hallgatók 20-23 év között (határokkal), akik nem budapestiek.
        static void Feladat04()
        {
            var result = SampleData.Students.Where(stud => stud.Age > 20 && stud.Age < 23 && stud.City != "Budapest");
            foreach (var á in result)
            {
                Console.WriteLine(á);
            }
        }

        // ---------- 2. Vetítés — Select, SelectMany ----------

        // 5. Csak a hallgatók nevei.
        static void Feladat05()
        {
            var result = SampleData.Students.Select(studs => studs.Name);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }



        }

        // 6. Anonim típusú lista: Name, GradeAverage.
        static void Feladat06()
        {
            var result = SampleData.Students
                .Select(s => new { s.Name, s.GradeAverage })
                .ToList();
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Name}: {item.GradeAverage:0.00}");
            }


        }

        // 7. Kurzus neve + a kurzust tartó tanár neve (Select, Join nélkül).
        static void Feladat07()
        {
            var result = SampleData.Courses.Select(cr => new { cr.Name, cr.TeacherId }).ToList();
            foreach (var item in result)
            {
                var teacher = SampleData.Teachers.First(teach => teach.Id == item.TeacherId);
                Console.WriteLine(item.Name + teacher.Name);
            }


        }

        // 8. SelectMany: beiratkozások lapos listája hallgató névvel.
        static void Feladat08()
        {
            var result = SampleData.Students
       .SelectMany(
           student => SampleData.Enrollments.Where(en => en.StudentId == student.Id),
           (student, enrollment) => new
           {
               StudentName = student.Name,
               enrollment.CourseId,
               enrollment.Grade
           })
       .ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"{item.StudentName} - CourseId: {item.CourseId}, Grade: {item.Grade}");
            }
        }

        // ---------- 3. Rendezés — OrderBy, ThenBy, Reverse ----------

        // 9. Hallgatók átlag szerint csökkenő sorrendben.
        static void Feladat09()
        {
            var result = SampleData.Students
                .OrderByDescending(s => s.GradeAverage)
                .ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Name}: {item.GradeAverage:0.00}");
            }
        }

        // 10. Hallgatók város szerint, majd név szerint növekvő sorrendben.
        static void Feladat10()
        {
            var result = SampleData.Students
                .OrderBy(s => s.City)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"{item.City} - {item.Name}");
            }
        }

        // 11. Kurzusok eredeti sorrendjének megfordítása (Reverse).
        static void Feladat11()
        {
            var result = SampleData.Courses.AsEnumerable()
                .Reverse()
                .ToList();

            foreach (var t in result)
            {
                Console.WriteLine(t);
            }

        }

        // ---------- 4. Csoportosítás — GroupBy ----------

        // 12. Hallgatók száma városonként.
        static void Feladat12()
        {
            // TODO
        }

        // 13. Átlagos tanulmányi átlag városonként.
        static void Feladat13()
        {
            var result = SampleData.Students
                .GroupBy(s => s.City)
                .Select(g => new { City = g.Key, AverageGrade = g.Average(s => s.GradeAverage) })
                .ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"{item.City}: {item.AverageGrade:0.00}");
            }
        }

        // 14. Kurzusnevek kategóriánként.
        static void Feladat14()
        {
            var result = SampleData.Courses
                .GroupBy(c => c.Category)
                .Select(g => new { Category = g.Key, CourseNames = g.Select(c => c.Name).ToList() })
                .ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Category}: {string.Join(", ", item.CourseNames)}");
            }
        }

        // ---------- 5. Összekapcsolás — Join, GroupJoin ----------

        // 15. Enrollments + Students Join: hallgató neve minden beiratkozáshoz.
        static void Feladat15()
        {
            var result = SampleData.Enrollments
                .Join(SampleData.Students,
                      e => e.StudentId,
                      s => s.Id,
                      (e, s) => new { StudentName = s.Name, e.CourseId, e.Grade })
                .ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"{item.StudentName} - CourseId: {item.CourseId}, Grade: {item.Grade}");
            }
        }

        // 16. Háromtáblás Join: hallgató neve, kurzus neve, érdemjegy.
        static void Feladat16()
        {
         
        }

        // 17. GroupJoin: hallgatónként a beiratkozásai (azok is, akiknek nincs).
        static void Feladat17()
        {
          
        }

        // ---------- 6. Halmazműveletek — Distinct, Union, Intersect, Except, Concat, Zip ----------

        // 18. Hány különböző város van a hallgatók között (Distinct).
        static void Feladat18()
        {
            var distinctCities = SampleData.Students
                .Select(s => s.City)
                .Distinct()
                .ToList();

            Console.WriteLine($"Distinct cities count: {distinctCities.Count}");
            foreach (var city in distinctCities)
            {
                Console.WriteLine(city);
            }
        }

        // 19. Különböző kurzuskategóriák (Distinct).
        static void Feladat19()
        {
            var categories = SampleData.Courses
                .Select(c => c.Category)
                .Distinct()
                .ToList();

            Console.WriteLine("Course categories:");
            foreach (var cat in categories)
            {
                Console.WriteLine(cat);
            }
        }

        // 20. Union, Intersect, Except a "kiváló" (átlag >= 4.5) és "budapesti" hallgatók nevei között.
        static void Feladat20()
        {
            
        }

        // 21. Concat: Matematika + Informatika kurzusnevek.
        static void Feladat21()
        {
            var matematika = SampleData.Courses
                .Where(c => c.Category == "Matematika")
                .Select(c => c.Name);

            var informatika = SampleData.Courses
                .Where(c => c.Category == "Informatika")
                .Select(c => c.Name);

            var concatenated = matematika.Concat(informatika).ToList();

            Console.WriteLine("Math + Informatics courses:");
            foreach (var name in concatenated) Console.WriteLine(name);
        }

        // 22. Zip: első 4 hallgató neve + első 4 kurzus neve párban.
        static void Feladat22()
        {
            var students = SampleData.Students
                .Select(s => s.Name)
                .Take(4)
                .ToList();

            var courses = SampleData.Courses
                .Select(c => c.Name)
                .Take(4)
                .ToList();

            var zipped = students.Zip(courses, (stu, course) => new { Student = stu, Course = course }).ToList();

            foreach (var pair in zipped)
            {
                Console.WriteLine($"{pair.Student}  -  {pair.Course}");
            }
        }

        // ---------- 7. Aggregálás — Count, Sum, Average, Min, Max, Aggregate ----------

        // 23. Hallgatók száma összesen, illetve akiknek átlaga > 4.0 (Count).
        static void Feladat23()
        {
            var total = SampleData.Students.Count();
            var above4 = SampleData.Students.Count(s => s.GradeAverage > 4.0);

            Console.WriteLine($"Total students: {total}");
            Console.WriteLine($"Students with average > 4.0: {above4}");
        }

        // 24. Az összes kurzus kredit-összege (Sum).
        static void Feladat24()
        {
            var totalCredits = SampleData.Courses.Sum(c => c.Credit);
            Console.WriteLine($"Total credits: {totalCredits}");
        }

        // 25. Hallgatók átlagéletkora (Average).
        static void Feladat25()
        {
            var avgAge = SampleData.Students.Average(s => s.Age);
            Console.WriteLine($"Average student age: {avgAge:0.00}");
        }

        // 26. Legfiatalabb és legidősebb hallgató életkora (Min, Max).
        static void Feladat26()
        {
            var minAge = SampleData.Students.Min(s => s.Age);
            var maxAge = SampleData.Students.Max(s => s.Age);
            Console.WriteLine($"Youngest: {minAge}, Oldest: {maxAge}");
        }

        // 27. Aggregate: hallgatónevek vesszővel elválasztva egy stringbe.
        static void Feladat27()
        {
            var joined = SampleData.Students
                .Select(s => s.Name)
                .Aggregate((a, b) => a + ", " + b);

            Console.WriteLine(joined);
        }

        // ---------- 8. Elemkiválasztás — First, Last, Single, ElementAt ----------

        // 28. Első szegedi hallgató (First/FirstOrDefault).
        static void Feladat28()
        {
            var firstSzeged = SampleData.Students.FirstOrDefault(s => s.City == "Szeged");
            if (firstSzeged != null)
                Console.WriteLine(firstSzeged);
            else
                Console.WriteLine("No student from Szeged found.");
        }

        // 29. Az egyetlen "Lakatos Kata" nevű hallgató (Single/SingleOrDefault),
        //     majd egy olyan eset kipróbálása try-catch-csel, ahol több találat van.
        static void Feladat29()
        {
            try
            {
                var single = SampleData.Students.Single(s => s.Name == "Lakatos Kata");
                Console.WriteLine($"Single found: {single}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding single Lakatos Kata: {ex.Message}");
            }

            // deliberate multiple-match case to demonstrate exception
            try
            {
                var multiple = SampleData.Students.Single(s => s.Name.Contains("a"));
                Console.WriteLine($"This won't print: {multiple}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Expected exception for multiple matches: {ex.Message}");
            }
        }

        // 30. A 3. indexű (0-tól) hallgató (ElementAt).
        static void Feladat30()
        {
            try
            {
                var thirdIndex = SampleData.Students.ElementAt(3);
                Console.WriteLine(thirdIndex);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Index out of range.");
            }
        }

        // ---------- 9. Particionálás — Skip, Take, SkipWhile, TakeWhile, Chunk ----------

        // 31. TOP 3 hallgató átlag szerint (Take).
        static void Feladat31()
        {
         
        }

        // 32. Az első 3 utáni hallgatók (Skip).
        static void Feladat32()
        {
         
        }

        // 33. Életkor szerint rendezve: TakeWhile (21 évnél fiatalabbak), majd SkipWhile (a többi).
        static void Feladat33()
        {
           
        }

        // 34. Hallgatók felbontása 4 fős csoportokra (Chunk).
        static void Feladat34()
        {
     
        }

        // ---------- 10. Egyéb — Any, All, Contains, ToDictionary, ToHashSet, DefaultIfEmpty ----------

        // 35. Van-e hallgató 2.5 alatti átlaggal (Any).
        static void Feladat35()
        {
            
        }

        // 36. Minden hallgató 18 évesnél idősebb-e (All).
        static void Feladat36()
        {
            
        }

        // 37. Szerepel-e "Pécs" a városok között (Contains).
        static void Feladat37()
        {
            var cities = SampleData.Students.Select(s => s.City);
            var containsPecs = cities.Contains("Pécs");
            Console.WriteLine($"Contains 'Pécs': {containsPecs}");
        }

        // 38. Dictionary<int, string> a hallgatók Id-je és neve alapján (ToDictionary).
        static void Feladat38()
        {
           
        }

        // 39. HashSet<string> a kurzuskategóriákból (ToHashSet).
        static void Feladat39()
        {
            
        }

        // 40. Nem létező kurzushoz tartozó beiratkozások, DefaultIfEmpty kezeléssel.
        static void Feladat40()
        {
        }
    }
}
