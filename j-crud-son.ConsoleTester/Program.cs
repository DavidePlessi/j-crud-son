using System;
using System.Collections.Generic;
using System.Linq;
using j_crud_son.Tests;

namespace j_crud_son.ConsoleTester
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var comuneService = new ComuneService();
            TestInsert(comuneService);
            TestUpdate(comuneService);
        }

        private static void TestInsert(ComuneService comuneService)
        {
            var comuneDaInserire = new Comune
            {
                Code = "Bolo",
                Description = "ByNight",
                Order = 1,
                NotActive = "true"
            };
            
            var newId = comuneService.NextId();
            var idNuovoComune = comuneService.Save(comuneDaInserire);
            var comuneInserito = comuneService.Load(idNuovoComune);

            if (comuneInserito == null)
            {
                Log("TestInsert", "Comune non inserito");
                return;
            }
            var errori = new List<string>();
            
            if(newId != idNuovoComune || idNuovoComune != comuneInserito.Id)
                errori.Add("Errore nella generazione dell'Id");
            
            errori.AddRange(FindDifferences(comuneInserito, comuneDaInserire));
            
            Log("TestInsert", errori.Any() 
                ? $"Errori: {string.Join("; ", errori)}" 
                : "Comune inserito correttamente");
        }

        private static void TestUpdate(ComuneService comuneService)
        {
            var comuneDaModificare = comuneService.Load().OrderBy(x => x.Id).FirstOrDefault();
            if (comuneDaModificare == null) return;
            
            var idComuneSelezionato = comuneDaModificare.Id;
            comuneDaModificare.Code += "ModificaCode";
            comuneDaModificare.Order += 3;
            comuneDaModificare.NotActive = comuneDaModificare.NotActive == "false" ? "true" : "false";
            comuneDaModificare.Description += comuneDaModificare.Description;
            
            var idModificato = comuneService.Save(comuneDaModificare);

            var comuneModificato = comuneService.Load(idComuneSelezionato);

            if (comuneModificato == null)
            {
                Log("TestUpdate", "Comune non trovato");
                return;
            }
            
            
            var errori = new List<string>();
            if(idModificato != idComuneSelezionato)
                errori.Add("Errore l'id è cambiato''");

            errori.AddRange(FindDifferences(comuneModificato, comuneDaModificare));
            
            
            Log("TestUpdate", errori.Any() 
                ? $"Errori: {string.Join("; ", errori)}" 
                : "Comune modificato correttamente");
        }

        public static void Log(string caller, string message)
        {
            Console.WriteLine($"{DateTime.Now:dd/MM/yyy HH.mm}: {caller} -> {message}");
        }

        public static List<string> FindDifferences(Comune a, Comune b)
        {
//            var nuoviErrori = (
//                from property in a.GetType().GetProperties() 
//                where property.GetValue(a) != property.GetValue(b) &&
//                      property.Name != "Id"
//                select $"{property.Name} diversa"
//            ).ToList();
            var nuoviErrori = new List<string>();
            
            if (a.Code != b.Code)
                nuoviErrori.Add("Code diverso");
            if (a.Description != b.Description)
                nuoviErrori.Add("Description diverso");
            if (a.NotActive != b.NotActive)
                nuoviErrori.Add("NotActive diverso");
            if (a.Order != b.Order)
                nuoviErrori.Add("Code diverso");
            
            return nuoviErrori;
        }
    }
}