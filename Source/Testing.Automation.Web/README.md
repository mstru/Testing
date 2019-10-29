# Automation.Framework 

Automation.Framework je riešenie pripravené pre automatizované testovanie webového rozhrania pomocou technológií .NET, SpecFlow a Selenium Webdriver

Príklad (Prihlásenie sa do aplikácie)

    [Binding]
    public class Login
    {
        [Given(@"Prihlasenie sa do aplikacie")]
        public void GivenPrihlasenieSaDoAplikacie()
        {
            Threaded<Session>
                .NavigateTo<LoggedInPage>()
                .Login(
                    EnvironmentSettings.Username, 
                    EnvironmentSettings.Password);
        }
    }
 
 Príklad (Navigacia na stránku)
 
    [Binding]
    public class Navigate
    {
        [Given(@"Navigacia na stranku, mal by som vidiet testovanu stranku")]
        public void GivenNavigaciaNaStrankuMalBySomVidietTestovanuStranku()
        {
            Threaded<Session>
                .NavigateTo<NavigationPage>(
                    EnvironmentSettings.Modul, 
                    EnvironmentSettings.Page);
        }
    }
    
    



Príklad (Filtrovanie v gride):

    [Binding]
    public class ListsPageFilterTest
    {
        [When(@"Filter: Nastavenie filtra tabulky nad jednotlivymi atributmi a overenie zaznamu v gride")]
        public void WhenFilterNastavenieFiltraTabulkyNadJednotlivymiAtributmiAOverenieZaznamuVGride()
        {
            Services.MarketParticipantsService svc = new Services.MarketParticipantsService();
            var info = svc.BindingData()
                .First();

            Threaded<Session>
                .CurrentBlock<ToolBarSearchPage>()
                    .ButtonFilter.Click<ListsFilterFormMap>()
                    .NameTextBoxBy.EnterText(info.NAZOV_SUBJEKTU)
                    .EicTextBoxBy.EnterText(info.EIC)
                    .ValidityStartEqTextBoxBy.EnterDate((DateTime)info.OBDOBIE_OD)
                    .ValidityEndEqTextBoxBy.EnterDate((DateTime)info.OBDOBIE_DO)
                    .TableFilterPage
                    .ButtonAccept.Click<TableGridPage>()
                        .Table
                            .VerifyThat(x => x.Row
                                .Count()
                                .Should()
                                .Be(1))
                            .VerifyThat(x => x.Row
                                .First()["Col2"]
                                .Should()
                                .Be(info.NAZOV_SUBJEKTU));
                            .VerifyThat(x => x.Row
                                .First()["Col3"]
                                .Should()
                                .Be(info.EIC));
        }
    }

Príklad (Exportovanie v gride):

    [Binding]
    public class ListsPageExportTest
    {
        [When(@"Export zaznamov a overenie suboru v adresari")]
        public void WhenExportZaznamovAOverenieSuboruVAdresari()
        {
            Threaded<Session>
              .CurrentBlock<ToolBarExportPage>()
                  .ExportToExcelButtonBy.Click()
                      .VerifyThat(e => e.WaitFileOfGivenFormat(FileFormat.xlsx)
                          .Count
                          .Should().Be(1))
                      .Delete()
                  .ExportToCsvButtonBy.Click()
                      .VerifyThat(e => e.WaitFileOfGivenFormat(FileFormat.zip)
                          .Count
                          .Should().Be(1))
                      .Delete();
        }
    }
