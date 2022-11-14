using ClassGenerator.Modeles;
using ClassGenerator.VuesModeles;

namespace ClassGenerator.Vues;

public partial class CreationAttributVue : ContentPage
{
    CreationAttributVueModele vueModele;
    public CreationAttributVue()
	{
		InitializeComponent();
        BindingContext = vueModele = new CreationAttributVueModele();
    }

    private void btnGenerate_Clicked(object sender, EventArgs e)
    {
        vueModele.GenerateClass(entryNomClasse.Text);
    }

    private void btnAddAttribute_Clicked(object sender, EventArgs e)
    {
        if (
            entryNomAttribut.Text == string.Empty || 
            typeAttribut.SelectedItem == null)
        {
            return;
        }
        vueModele.AddAttribute(entryNomAttribut.Text, ((Typage)typeAttribut.SelectedItem).Nom, entryDefaultValueAttribut.Text, cbIsSetter.IsChecked, cbIsGetter.IsChecked, cbIsConstruct.IsChecked, cbIsList.IsChecked);
        entryNomAttribut.Text = String.Empty;
        entryDefaultValueAttribut.Text = String.Empty;
        typeAttribut.SelectedItem = null;
        cbIsConstruct.IsChecked = false;
        cbIsList.IsChecked = false;
        cbIsGetter.IsChecked = false;
        cbIsSetter.IsChecked = false;
        vueModele.LesAttributs = vueModele.classe.Attributs;
    }

    private void btnAddMethode_Clicked(object sender, EventArgs e)
    {
        if (entryNomMethode.Text == String.Empty) return;
        vueModele.AddMethode(entryNomMethode.Text);
        entryNomMethode.Text = String.Empty;
    }
}