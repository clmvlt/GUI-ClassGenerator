using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator.Modeles
{
    public class Structure
    {
        #region Attributes
        private string _nom;
        private List<Property> _properties;
        private List<Attribut> _attributs;
        private List<Constructeur> _constructeurs;
        private List<Methode> _methodes;

        public static List<Structure> CollClass = new List<Structure>();
        #endregion

        #region Constructeurs
        public Structure(string nom)
        {
            _nom = nom;
            _properties = new List<Property>();
            _attributs = new List<Attribut>();
            _constructeurs = new List<Constructeur>();
            _methodes = new List<Methode>();

            CollClass.Add(this);
        }
        #endregion

        #region Properties (Getters/Setters)
        public string Nom { get => _nom; set => _nom = GetCamelCase(value); }
        public List<Attribut> Attributs { get => _attributs; set => _attributs = value; }

        #endregion

        #region Méthodes
        public string GetCamelCase(string nom)
        {
            nom = nom[0].ToString().ToUpper() + nom.Substring(1);
            return nom;
        }
        public Typage GetTypage(string type)
        {
            foreach (Typage t in Typage.CollClass)
                if (t.Nom.ToLower().Equals(type.ToLower())) return t;
            return null;
        }
        public void AddAttribute(Attribut at)
        {
            this._attributs.Add(at);
            if (at.IsGetter) this.AddGetter(at.Nom);
            if (at.IsSetter) this.AddSetter(at.Nom);
        }
        public void RemoveAttribute(Attribut at)
        {
            this._attributs.Remove(at);
            if (at.IsGetter) this.RemoveGetter(GetProperty(at.Nom));
            if (at.IsSetter) this.RemoveSetter(GetProperty(at.Nom));
        }
        public void AddMethode(Methode met) => this._methodes.Add(met);
        public void RemoveMethode(Methode met) => this._methodes.Remove(met);
        public void AddSetter(string nom) => this._properties.Add(new Setter(nom));
        public void RemoveSetter(Property set) => this._properties.Remove(set);
        public void AddGetter(string nom) => this._properties.Add(new Getter(nom));
        public void RemoveGetter(Property get) => this._properties.Remove(get);
        public Property GetProperty(string nom)
        {
            foreach (Property p in this._properties)
            {
                if (p.Nom == nom) return p;
            }
            return null;
        }

        private string GetAttribus()
        {
            string res = string.Empty;
            res += "\n#region Attributes\n";
            foreach (Attribut at in this._attributs)
                res += at.GetStringPrivate();
            res += "public static List<"+Nom+"> CollClass = new List<"+Nom+">();\n" +
                "#endregion\n";
            return res;
        }

        private string GetConstructeur()
        {
            string res = string.Empty;
            res += "\n#region Constructeur\npublic "+Nom+"(";
            foreach (Attribut at in this._attributs)
            {
                res += at.GetStringConstructSign();
                if (this._attributs.Last() != at) res += ",";
            }
            res += ")\n{\n";
            foreach (Attribut at in this._attributs)
            {
                res += at.GetStringConstruct();
            }
            res += "}\n#endregion\n";
            return res;
        }

        private string GetProperties()
        {
            string res = string.Empty;
            res += "\n#region Properties (Getters/Setters)\n";
            foreach (Attribut at in this._attributs)
            {
                res += at.GetStringProperty();
            }
            res += "#endregion\n";
            return res;
        }

        private string GetMethodes()
        {
            string res = string.Empty;
            res += "#region Methodes\n";
            foreach (Methode met in _methodes)
            {
                res += met.GetStringMethode();
            }
            res += "#endregion\n";
            return res;
        }

        public string Generate()
        {
            string res = string.Empty;
            res += "class " + Nom + "\n{\n";
            res += GetAttribus();
            res += GetConstructeur();
            res += GetProperties();
            res += GetMethodes();
            res += "\n}";
            return res;
        }
        #endregion
    }
}
