using System;
using System.Collections.Generic;
using System.Linq;
using SisVetSanMiguel.Domain.Interfaces;
using SisVetSanMiguel.Domain.Models;


namespace SisVetSanMiguel.Services;

public class VeterinariaService : IGeneralCrud<Vet>
{
    private readonly List<Vet> _veterinarios = new();

    public IEnumerable<Vet> GetAll()
    {
        return _veterinarios;
    }

    public Vet? GetById(int id)
    {
        return _veterinarios.FirstOrDefault(v => v.Id == id);
    }

    public void Add(Vet vet)
    {
        _veterinarios.Add(vet);
    }

    public void Update(Vet vet)
    {
        var existente = _veterinarios.FirstOrDefault(v => v.Id == vet.Id);
        if (existente != null)
        {
            existente.Name = vet.Name;
            existente.Document = vet.Document;
            existente.Email = vet.Email;
            existente.Atentions = vet.Atentions;
        }
    }

    public void Delete(int id)
    {
        var vet = _veterinarios.FirstOrDefault(v => v.Id == id);
        if (vet != null)
        {
            _veterinarios.Remove(vet);
        }
    }
}



