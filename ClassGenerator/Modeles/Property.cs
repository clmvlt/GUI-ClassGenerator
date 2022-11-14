using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator.Modeles
{
    public abstract class Property
    {
        #region Attributes
        private string _nom;
        #endregion

        #region Constructeurs
        public Property(string nom)
        {
            _nom = nom;
        }
        #endregion

        #region Properties (Getters/Setters)
        public string Nom { get => _nom; set => _nom = value; }
        #endregion

        #region Méthodes
        #endregion
    }
}
