using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator.Modeles
{
    public class Attribut
    {
        #region Attributes
        private bool _isSetter;
        private bool _isGetter;
        private bool _isConstruct;
        private string _nom;
        private string _defaultValue;
        private Typage _type;
        private bool _isList;
        #endregion

        #region Constructeurs
        public Attribut(bool isSetter, bool isGetter, bool isConstruct, string nom, string defaultValue, Typage type, bool isList)
        {
            _isSetter = isSetter;
            _isGetter = isGetter;
            _isConstruct = isConstruct;
            _nom = nom;
            _defaultValue = defaultValue;
            _type = type;
            _isList = isList;
        }
        #endregion

        #region Properties (Getters/Setters)
        public bool IsSetter { get => _isSetter; set => _isSetter = value; }
        public bool IsGetter { get => _isGetter; set => _isGetter = value; }
        public bool IsConstruct { get => _isConstruct; set => _isConstruct = value; }
        public string Nom { get => _nom; set => _nom = value; }
        public string DefaultValue { get => _defaultValue; set => _defaultValue = value; }
        public Typage Type { get => _type; set => _type = value; }
        public bool IsList { get => _isList; set => _isList = value; }

        #endregion

        #region Méthodes
        public string AddUnderscore(string nom)
        {
            if (nom[0] != '_') nom = nom.Insert(0, "_");
            return nom;
        }

        public string RemoveUnderscore(string nom)
        {
            if (nom[0] == '_') nom = nom.Substring(1);
            return nom;
        }

        public string GetNomPrivate(string nom)
        {
            nom = AddUnderscore(nom);
            nom = "_" + nom[1].ToString().ToLower() + nom.Substring(2);
            return nom;
        }

        public string GetNom(string nom)
        {
            nom = RemoveUnderscore(nom);
            nom = nom[0].ToString().ToLower() + nom.Substring(1);
            return nom;
        }

        public string GetCamelCase(string nom)
        {
            nom = RemoveUnderscore(nom);
            nom = nom[0].ToString().ToUpper() + nom.Substring(1);
            return nom;
        }

        public string GetStringPrivate()
        {
            return "private " + (IsList ? "List<"+Nom+">" : Type.Nom) + " " + GetNomPrivate(Nom) + (DefaultValue==null||DefaultValue==String.Empty?"":" = " + DefaultValue) + ";\n";
        }

        public string GetStringConstructSign()
        {
            if (IsList) return "List<" + Type.Nom + "> " + GetNom(Nom);
            return Type.Nom + " " + GetNom(Nom);
        }

        public string GetStringConstruct()
        {
            if (IsList) return GetNomPrivate(Nom) + " = new List<" + Type.Nom + ">();\n";
            return GetNomPrivate(Nom) + " = " + GetNom(Nom)+";\n";
        }

        public string GetStringProperty()
        {
            if (IsSetter ||IsGetter) return "public " + Type.Nom + " " + GetCamelCase(Nom) + "{ " + (IsGetter ? "{ get => "+GetNomPrivate(Nom)+"; }" : "") + (IsSetter ? "{ set => "+GetNomPrivate(Nom)+" = value; }" : "") + " }\n";
            return "";
        }
        #endregion
    }
}
