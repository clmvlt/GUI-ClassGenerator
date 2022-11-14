using ClassGenerator.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator.VuesModeles
{
    public class CreationAttributVueModele : BaseVueModele
    {
        #region Attributes
        public Structure classe = new Structure("classe");
        private string _stringGenerated;
        private List<Attribut> _attributs;
        #endregion

        #region Constructeurs
        public CreationAttributVueModele()
        {
            new Typage("int");
            new Typage("string");
            new Typage("bool");
            new Typage("float");
        }
        #endregion

        #region Properties (Getters/Setters)
        public List<Typage> LesTypes
        {
            get => Typage.CollClass;
            set => SetProperty(ref Typage.CollClass, value);
        }
        public string StringGenerated
        {
            get => _stringGenerated;
            set => SetProperty(ref _stringGenerated, value);
        }
        public List<Attribut> LesAttributs
        {
            get => _attributs;
            set => SetProperty(ref _attributs, value);

        }
        #endregion

        #region Méthodes
        public void GenerateClass(string nomClasse)
        {
            classe.Nom = nomClasse;
            StringGenerated = classe.Generate();
        }

        public void AddAttribute(string nom, string type, string defaultValue, bool isSetter, bool isGetter, bool isConstruct, bool isList)
        {
            classe.AddAttribute(new Attribut(isSetter,  isGetter, isConstruct, nom, defaultValue, classe.GetTypage(type), isList));
        }

        public void AddMethode(string nom)
        {
            classe.AddMethode(new Methode(nom));
        }
        #endregion
    }
}
