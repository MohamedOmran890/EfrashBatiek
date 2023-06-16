using Castle.Components.DictionaryAdapter;
using EfrashBatek.Models;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.ViewModel
{
    public class DesignPhoteVM
    {
        public int Id { get; set; }
        public string Descrption { get; set; }
        public ICollection<Photo> photo{ get; set; }
    }
}
