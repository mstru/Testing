# Testing.Automation.API

Riešenie pripravené pre automatizované testovanie Web API rozhrania pomocou technológií .NET

Príklad (Vytvorenie entity pomocou metódy HttpPost)

	    [Given(@"Metoda POST /AlarmDef/ insert - definovanie alarmu typu = ""(.*)"" - result should Created")]
        public void GivenMetodaPOSTAlarmDefInsert_DefinovanieAlarmuTypu_ResultShouldCreated(string alarmType)
        {
            string body = string.Empty;

            alarmDefName = string.Format("Testing API {0} - {1}", alarmType, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            if (alarmType == _polygonAlarmType.Name)
            {
                body = "{'@odata.type': '#AlarmDefPolygonEntity'," +
                       "'typeId': "+ _polygonAlarmType.Id + "," + 
                       "'name': '" + alarmDefName + "'," +
                       "'polygonId': " + _polygonAlarmData.PolygonId + "," + 
                       "'enabled': true," +
                       "'notificationEnabled': true," +
                       "'wagons': " +
                       "[{" +
                            "'wagonId': " + _polygonAlarmData.Wagons.First().WagonId + "" +
                       "}]}";
            }
            else if (alarmType == _standingAlarmType.Name)
            {
                body = "{'@odata.type': '#AlarmDefStandingEntity'," +
                       "'typeId': " + _standingAlarmType.Id + "," +
                       "'name': '" + alarmDefName + "'," +
                       "'polygonId': " + _standingAlarmData.PolygonId + "," +
                       "'enabled': true," +
                       "'notificationEnabled': true," +
                       "'standingDuration': 'P1D'," +
                       "'wagons': " +
                       "[{" +
                            "'wagonId': " + _standingAlarmData.Wagons.First().WagonId + "" +
                       "}]}";
            }
            else if (alarmType == _shockDetectedAlarmType.Name)
            { 
                body = "{'@odata.type': '#AlarmDefShockEntity'," +
                       "'typeId': "+ _shockDetectedAlarmType.Id + "," +
                       "'name': '" + alarmDefName + "'," +
                       "'polygonId': " + _shockAlarmData.PolygonId + "," +
                       "'enabled': true," +
                       "'notificationEnabled': true," +
                       "'shockX': 1.5," +
                       "'shockY': null," +
                       "'shockZ': 2.5," +
                       "'wagons': " +
                       "[{" +
                            "'wagonId': " + _shockAlarmData.Wagons.First().WagonId + "" +
                       "}]}";
            }

            MyWebApi
               .Start()
                   .Working()
                   .WithHttpRequestMessage(req => req.WithMethod(HttpMethod.Post)
                   .WithRequestUri($"/v2/{RoutePrefixConst}")
                   .WithBody(body))
                   .ShouldReturnHttpResponseMessage()
                        .ValidWithStatusCode(HttpStatusCode.Created);
        }


Príklad (Kontrola vytvorenia entity pomocou metódy HttpGet)

        [When(@"Metoda GET /AlarmDef/ kontrola vrati dotazovatelny objekt - result should OK")]
        public void WhenMetodaGETAlarmDefKontrolaVratiDotazovatelnyObjekt_ResultShouldOK(Table table)
        {
            string uri = $"/v2/{RoutePrefixConst}?$filter=name eq '{alarmDefName}'";

            var result = MyWebApi
               .Start()
                   .Working()
                   .WithHttpRequestMessage(req => req.WithMethod(HttpMethod.Get)
                   .WithRequestUri(uri))
                   .ShouldReturnHttpResponseMessage()
                        .ValidWithStatusCode(HttpStatusCode.OK)
				.CheckShouldEqual(new Dictionary<string, IEnumerable<string>>
				 {
				    { "typeId", new List<string> { TypeId } }, { "name", new List<string> { Name } }, { "polygonId", new List<string> { PolygonId } }
				 });
        }
