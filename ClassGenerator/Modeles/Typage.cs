using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator.Modeles
{
    public class Typage
    {
        #region Attributes
        private string _nom;

        public static List<Typage> CollClass = new List<Typage>();
        #endregion

        #region Constructeurs
        public Typage(string nom)
        {
            _nom = nom;

            CollClass.Add(this);
        }
        #endregion

        #region Properties (Getters/Setters)
        public string Nom { get => _nom; set => _nom = value; }
        #endregion

        #region Méthodes
        #endregion
    }
}
