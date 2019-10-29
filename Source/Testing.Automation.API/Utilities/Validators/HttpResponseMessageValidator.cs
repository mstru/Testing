namespace Testing.Automation.API.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Testing.Automation.Reporter;
    using Testing.Automation.Reporter.Exceptions;
    using Builders.Helper;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using FluentAssertions;
    using Testing.Automation.API.Common.Extensions;

    /// <summary>
    /// Trieda validátor obsahujúca logiku overenia správ HTTP.
    /// </summary>
    public static class HttpResponseMessageValidator
    {
 
        /// <summary>
        /// Testuje, či je obsah správy HTTP odpovede určitého typu.
        /// </summary>
        /// <typeparam name="TContentType">Type of expected HTTP content.</typeparam>
        /// <param name="content">Actual HTTP content.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void WithContentOfType<TContentType>(
            HttpContent content,
            Action<string, string, string> failedValidationAction)
            where TContentType : HttpContent
        {
            var expectedType = typeof(TContentType);
            var actualType = content.GetType();
            if (Reflection.AreNotAssignable(actualType, expectedType))
            {
                failedValidationAction(
                    "content",
                    string.Format("to be {0}", expectedType.ToFriendlyTypeName()),
                    string.Format("was in fact {0}", actualType.ToFriendlyTypeName()));
            }
        }

        /// <summary>
        /// Testuje, ci obsah spravy HTTP odpovede obsahuje poskytnutý reťazec a nie je prazdny.
        /// </summary>
        /// <param name="actualContent">Actual HTTP content.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ContainingResponse(
            HttpContent actualContent,
            Action<string, string, string, string> failedValidationAction)
        {
            var actualContentAsString = actualContent.ReadAsStringAsync().Result;
            int actualJSONObjectCount = JSONHelper.ResponseParserCount(actualContentAsString);

            if (actualContentAsString == null || actualJSONObjectCount == 0)
            {
                failedValidationAction(
                    "json actual content",
                    "to be not empty",
                    string.Format("was in fact actual content as string {0} or actual node {1}", actualContentAsString, actualJSONObjectCount), string.Empty);
            }
        }


        public static void ContainingContentReponse(
            JArray actualArrayContent,
            Action<string, string, string, string> failedValidationAction)
        {
            var actualResponseValuesCount = actualArrayContent.Count();

            if (actualResponseValuesCount == 0)
            {
                failedValidationAction(
                    "json content ",
                    "to be initialized and set ",
                    "it was null and no data were found", 
                    string.Empty);
            }
        }

        /// <summary>
        /// Testuje, či obsah správy HTTP odpovede obsahuje poskytnutý reťazec.
        /// </summary>
        /// <param name="actualContent">Actual HTTP content.</param>
        /// <param name="expectedContent">Expected string content.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void WithStringContent(
            HttpContent actualContent,
            string expectedContent,
            Action<string, string, string> failedValidationAction)
        {
            var actualContentAsString = actualContent.ReadAsStringAsync().Result;
            if (expectedContent != actualContentAsString)
            {
                failedValidationAction(
                    "string content",
                    string.Format("to be '{0}'", expectedContent),
                    string.Format("was in fact '{0}'", actualContentAsString));
            }
        }

        /// <summary>
        /// Overenie obsahu protokolu HTTP pre nulovú referenciu.
        /// </summary>
        /// <param name="content">HTTP content to validate.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateContent(HttpContent content, Action<string, string, string> failedValidationAction)
        {
            if (content == null)
            {
                failedValidationAction(
                    "content",
                    "to be initialized and set",
                    "it was null and no content headers were found");
            }
        }

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku odpovede s určitým názvom.
        /// </summary>
        /// <param name="headers">HTTP headers.</param>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="isContentHeader">Indicates whether the header is content header.</param>
        public static void ContainHeaderType(
            HttpContent content,
            string name,
            Action<string, string, string> failedValidationAction,
            bool isContentHeader = false)
        {
            if (content.Headers.ContentType.ToString() != name)
            {
                failedValidationAction(
                    isContentHeader ? "content headers" : "headers",
                    string.Format("to contain {0}", name),
                    "none was found");
            }
        }

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku odpovede s urcitou dlzkou.
        /// </summary>
        /// <param name="headers">HTTP headers.</param>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="isContentHeader">Indicates whether the header is content header.</param>
        public static void ContainLength(
            HttpContent content,
            Action<string, string, string> failedValidationAction,
            bool isContentHeader = false)
        {
            if (content.Headers.ContentLength == 0)
            {
                failedValidationAction(
                    isContentHeader ? "content length" : "length",
                    string.Format("to contain length {0}", content.Headers.ContentLength),
                    "none was found");
            }
        }


        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku odpovede s určitým názvom.
        /// </summary>
        /// <param name="headers">HTTP headers.</param>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="isContentHeader">Indicates whether the header is content header.</param>
        public static void ContainingHeader(
            HttpHeaders headers,
            string name,
            Action<string, string, string> failedValidationAction,
            bool isContentHeader = false)
        {
            if (!headers.Contains(name))
            {
                failedValidationAction(
                    isContentHeader ? "content headers" : "headers",
                    string.Format("to contain {0}", name),
                    "none was found");
            }
        }

        /// <summary>
        /// Testuje, či odpoveď HTTP obsahuje hlavičku odpovede s určitým názvom a hodnotou.
        /// </summary>
        /// <param name="headers">HTTP headers.</param>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="value">Value of expected response header.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="isContentHeader">Indicates whether the header is content header.</param>
        public static void ContainingHeader(
            HttpHeaders headers,
            string name,
            string value,
            Action<string, string, string> failedValidationAction,
            bool isContentHeader = false)
        {
            ContainingHeader(headers, name, failedValidationAction);
            var headerValue = GetHeaderValues(headers, name).FirstOrDefault(v => v == value);

            if (headerValue == null)
            {
                failedValidationAction(
                    isContentHeader ? "content headers" : "headers",
                    string.Format("to contain {0} with {1} value", name, value),
                    "none was found");
            }
        }

        public static JArray Check(
            HttpContent actualContent,
            Action<string, string, string, string> failedValidationAction)
        {
            ContainingResponse(actualContent, failedValidationAction);
            var actualResponseValuesToString = actualContent.ReadAsStringAsync().Result;
            var actualResponseValuesToArray = GetJsonContent(actualResponseValuesToString);
            ContainingContentReponse(actualResponseValuesToArray, failedValidationAction);

            return actualResponseValuesToArray;
        }

        /// <summary>
        /// Tvrdenie, ze objekt sa rovna inemu objektu pomocou System.Object.Equals
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Ocakavana hodnota objektu</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldEqual<T>(
            HttpContent actualContent,
            string name,
            T value,
            Action<string, string, string, string> failedValidationAction)
        {
            int i = 0;
            T actualObjectValue = default(T);
            var actualResponseValuesToArray = Check(actualContent, failedValidationAction);

            actualResponseValuesToArray.Children().ForEach(arg =>
            {
                actualObjectValue = GetJsonToken<T>(arg.Value<JToken>(), name);
                
                bool result = CommonValidator.ValidateBinding(() => actualObjectValue.Should().Be(value));

                if (!result)
                {
                    failedValidationAction(
                         $"actual object value with path [JSON].{arg.Path}.{name}: '{actualObjectValue}' ",
                         $"should be Equal with expected value '{value}', ",
                         "but was in fact different!",
                         arg.Parent[i].ToString());
                }
                i++;
            });

            Reporter.Pass($"Check actual [JSON].[value].[start/end - 1/{actualResponseValuesToArray.Count}].{name}: '{actualObjectValue}' Equal '{value}' ", CodeBlockType.Label);
        }

        /// <summary>
        /// Tvrdenie, ze System.String je jednou zo specifikovanych values
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="values">Specifikovane hodnoty</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldBeOneOf(
            HttpContent actualContent,
            string name,
            IEnumerable<string> values,
            Action<string, string, string, string> failedValidationAction
            )
        {
            int i = 0;
            var actualObjectValue = default(string);
            var actualResponseValuesToArray = Check(actualContent, failedValidationAction);

            actualResponseValuesToArray.Children().ForEach(arg =>
            {
                actualObjectValue = GetJsonToken<string>(arg.Value<JToken>(), name);

                bool result = CommonValidator.ValidateBinding(() => actualObjectValue.Should().BeOneOf(values));

                if (!result)
                {
                    failedValidationAction(
                        $"actual object value with path [JSON].{arg.Path}.{name}: '{actualObjectValue}' ",
                        $"should Be One Of with expected values [ {string.Join(", ", values)} ], ",
                        $"but was in fact different!",
                        arg.Parent[i].ToString());
                }
                i++;
            });

            Reporter.Pass($"Check actual [JSON].[value].[start/end - 1/{actualResponseValuesToArray.Count}].{name}: '{actualObjectValue}' Be One Of '{string.Join(", ", values)}' ", CodeBlockType.Label);
        }

        /// <summary>
        /// Tvrdenie, ze pocet poloziek v kolekcii je mensi alebo rovnaky ako ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="value">Ocakavana hodnota objektu</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldHaveCountLessOrEqualTo(
            HttpContent actualContent,
            int value,
            Action<string, string, string, string> failedValidationAction)
        {
            var actualResponseValuesToArray = Check(actualContent, failedValidationAction);

            bool result = CommonValidator.ValidateBinding(() => actualResponseValuesToArray.Count.As<JArray>().Should().HaveCountLessOrEqualTo(value));

            if (!result)
            {
                failedValidationAction(
                    $"actual object values [{actualResponseValuesToArray.Count}] ",
                    $"should Have Count Less Or Equal To with expected [{value}], ",
                    $"but was in fact different!",
                    string.Empty);
            }

            Reporter.Pass($"Check actual [{actualResponseValuesToArray.Count}] should have count less or equal To [{value}] ", CodeBlockType.Label);
        }

        /// <summary>
        /// Tvrdi, ze objekt sa nerovna inemu objektu pomocou System.Object.Equals 
        /// </summary>
        /// <typeparam name="T">Typ objektu</typeparam>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldNotBe<T>(
            HttpContent actualContent,
            string name,
            T value,
            Action<string, string, string, string> failedValidationAction)
        {
            int i = 0;
            T actualObjectValue = default(T);
            var actualResponseValuesToArray = Check(actualContent, failedValidationAction);

            actualResponseValuesToArray.Children().ForEach(arg =>
            {
                actualObjectValue = GetJsonToken<T>(arg.Value<JToken>(), name);

                bool result = CommonValidator.ValidateBinding(() => actualObjectValue.Should().NotBe(value));

                if (!result)
                {
                    failedValidationAction(
                         $"actual object value with path [JSON].{arg.Path}.{name}: '{actualObjectValue}' ",
                         $"should not be with expected value '{value}', ",
                         "but was in fact different!",
                         arg.Parent[i].ToString());
                }
                i++;
            });

            Reporter.Pass($"Check actual [JSON].[value].[start/end - 1/{actualResponseValuesToArray.Count}].{name}: '{actualObjectValue}' Not be '{value}' ", CodeBlockType.Label);
        }

        /// <summary>
        /// Tvrdenie, ze ciselna hodnota je vyssia ako zadana ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldBeGreaterThan(
            HttpContent actualContent,
            string name,
            long value,
            Action<string, string, string, string> failedValidationAction)
        {
            int i = 0;
            var actualObjectValue = default(long);
            var actualResponseValuesToArray = Check(actualContent, failedValidationAction);

            actualResponseValuesToArray.Children().ForEach(arg =>
            {
                actualObjectValue = GetJsonToken<long>(arg.Value<JToken>(), name);

                bool result = CommonValidator.ValidateBinding(() => actualObjectValue.Should().BeGreaterThan(value));

                if (!result)
                {
                    failedValidationAction(
                         $"actual object value with path [JSON].{arg.Path}.{name}: '{actualObjectValue}' ",
                         $"should Be Greater Than with expected value '{value}', ",
                         "but was in fact different!",
                         arg.Parent[i].ToString());
                }
                i++;
            });

            Reporter.Pass($"Check actual [JSON].[value].[start/end - 1/{actualResponseValuesToArray.Count}].{name}: '{actualObjectValue}' Be Greater Than '{value}' ", CodeBlockType.Label);
        }

        /// <summary>
        /// Tvrdenie, ze ciselna hodnota je vacsia alebo rovna ako zadana ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldBeGreaterOrEqualTo(
            HttpContent actualContent,
            string name,
            long value,
            Action<string, string, string, string> failedValidationAction)
        {
            int i = 0;
            var actualObjectValue = default(long);
            var actualResponseValuesToArray = Check(actualContent, failedValidationAction);

            actualResponseValuesToArray.Children().ForEach(arg =>
            {
                actualObjectValue = GetJsonToken<long>(arg.Value<JToken>(), name);

                bool result = CommonValidator.ValidateBinding(() => actualObjectValue.Should().BeGreaterOrEqualTo(value));

                if (!result)
                {
                    failedValidationAction(
                         $"actual object value with path [JSON].{arg.Path}.{name}: '{actualObjectValue}' ",
                         $"should Be Greater Or Equal To with expected value '{value}', ",
                         "but was in fact different!",
                         arg.Parent[i].ToString());
                }
                i++;
            });

            Reporter.Pass($"Check actual [JSON].[value].[start/end - 1/{actualResponseValuesToArray.Count}].{name}: '{actualObjectValue}' Be Greater Or Equal To '{value}' ", CodeBlockType.Label);
        }

        /// <summary>
        /// Tvrdenie, ze ciselna hodnota je nizsia ako zadana ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldBeLessThan(
            HttpContent actualContent,
            string name,
            long value,
            Action<string, string, string, string> failedValidationAction)
        {
            int i = 0;
            var actualObjectValue = default(long);
            var actualResponseValuesToArray = Check(actualContent, failedValidationAction);

            actualResponseValuesToArray.Children().ForEach(arg =>
            {
                actualObjectValue = GetJsonToken<long>(arg.Value<JToken>(), name);

                bool result = CommonValidator.ValidateBinding(() => actualObjectValue.Should().BeLessThan(value));

                if (!result)
                {
                    failedValidationAction(
                         $"actual object value with path [JSON].{arg.Path}.{name}: '{actualObjectValue}' ",
                         $"should Be Less Than with expected value '{value}', ",
                         "but was in fact different!",
                         arg.Parent[i].ToString());
                }
                i++;
            });

            Reporter.Pass($"Check actual [JSON].[value].[start/end - 1/{actualResponseValuesToArray.Count}].{name}: '{actualObjectValue}' Be Less Than '{value}' ", CodeBlockType.Label);
        }

        /// <summary>
        /// Tvrdi, ze ciselna hodnota je mensia alebo rovna ako ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldBeLessOrEqualTo(
            HttpContent actualContent,
            string name,
            long value,
            Action<string, string, string, string> failedValidationAction)
        {
            int i = 0;
            var actualObjectValue = default(long);
            var actualResponseValuesToArray = Check(actualContent, failedValidationAction);

            actualResponseValuesToArray.Children().ForEach(arg =>
            {
                actualObjectValue = GetJsonToken<long>(arg.Value<JToken>(), name);

                bool result = CommonValidator.ValidateBinding(() => actualObjectValue.Should().BeLessOrEqualTo(value));

                if (!result)
                {
                    failedValidationAction(
                         $"actual object value with path [JSON].{arg.Path}.{name}: '{actualObjectValue}' ",
                         $"should Be Less Or Equal To with expected value '{value}', ",
                         "but was in fact different!",
                         arg.Parent[i].ToString());
                }
                i++;
            });

            Reporter.Pass($"Check actual [JSON].[value].[start/end - 1/{actualResponseValuesToArray.Count}].{name}: '{actualObjectValue}' Be Less Or Equal To '{value}' ", CodeBlockType.Label);
        }

        /// <summary>
        /// Tvrdi, ze retazec obsahuje specifikovane ocakavane hodnoty
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazvy property objektov</param>
        /// <param name="value">Specifikovane hodnoty</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldContainEquivalentOf(
            HttpContent actualContent,
            IEnumerable<string> names,
            long value,
            Action<string, string, string, string> failedValidationAction)
        {
            int i = 0;
            JToken actualObjectValue = default(long);
            var actualResponseValuesToArray = Check(actualContent, failedValidationAction);

            actualResponseValuesToArray.Children().ForEach(arg =>
            {
                actualObjectValue = GetJsonTokenNested<long>(arg.Value<JToken>(), names);

                bool result = CommonValidator.ValidateBinding(() => actualObjectValue.ToString().Should().ContainEquivalentOf(value.ToString()));

                if (!result)
                {
                    failedValidationAction(
                         $"actual object value with path [JSON].{arg.Path}.{string.Join(".", names)}: '{actualObjectValue}' ",
                         $"should Contain Equivalent Of with expected value '{value}', ",
                         "but was in fact different!",
                         arg.Parent[i].ToString());
                }
                i++;
            });

            Reporter.Pass($"Check actual [JSON].[value].[start/end - 1/{actualResponseValuesToArray.Count}].{string.Join(".", names)}: '{actualObjectValue}' Contain Equivalent Of '{value}' ", CodeBlockType.Label);
        }

        /// <summary>
        /// Tvrdi, ze aktualny System.DateTime je teraz alebo potom
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazvy property objektov</param>
        /// <param name="value">Specifikovane hodnoty</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldBeOnOrAfter(
            HttpContent actualContent,
            string name,
            DateTime value,
            Action<string, string, string, string> failedValidationAction)
        {
            int i = 0;
            var actualObjectValue = default(DateTime);
            var actualResponseValuesToArray = Check(actualContent, failedValidationAction);

            actualResponseValuesToArray.Children().ForEach(arg =>
            {
                actualObjectValue = GetJsonToken<DateTime>(arg.Value<JToken>(), name);

                bool result = CommonValidator.ValidateBinding(() => actualObjectValue.As<DateTime>().Should().BeOnOrAfter((DateTime)value));

                if (!result)
                {
                    failedValidationAction(
                        $"actual object value with path [JSON].{arg.Path}.{name}: '{actualObjectValue}' ",
                        $"should Be On Or After with expected value '{value}', ",
                        "but was in fact different!",
                        arg.Parent[i].ToString());
                }
                i++;
            });

            Reporter.Pass($"Check actual [JSON].[value].[start/end - 1/{actualResponseValuesToArray.Count}].{name}: '{actualObjectValue}' Be On Or After '{value}' ", CodeBlockType.Label);
        }

        /// <summary>
        /// Tvrdi, ze aktualny System.DateTime je teraz alebo predtym
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazvy property objektov</param>
        /// <param name="value">Specifikovane hodnoty</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldBeOnOrBefore(
            HttpContent actualContent,
            string name,
            DateTime value,
            Action<string, string, string, string> failedValidationAction)
        {
            int i = 0;
            var actualObjectValue = default(DateTime);
            var actualResponseValuesToArray = Check(actualContent, failedValidationAction);

            actualResponseValuesToArray.Children().ForEach(arg =>
            {
                actualObjectValue = GetJsonToken<DateTime>(arg.Value<JToken>(), name);

                bool result = CommonValidator.ValidateBinding(() => actualObjectValue.As<DateTime>().Should().BeOnOrBefore((DateTime)value));

                if (!result)
                {
                    failedValidationAction(
                        $"actual object value with path [JSON].{arg.Path}.{name}: '{actualObjectValue}' ",
                        $"should Be On Or Before with expected value '{value}', ",
                        "but was in fact different!",
                        arg.Parent[i].ToString());
                }
                i++;
            });

            Reporter.Pass($"Check actual [JSON].[value].[start/end - 1/{actualResponseValuesToArray.Count}].{name}: '{actualObjectValue}' Be On Or Before '{value}' ", CodeBlockType.Label);
        }

        /// <summary>
        /// Tvrdi, ze hodnota je true
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazvy property objektov</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldBeTrue(
            HttpContent actualContent,
            string name,
            Action<string, string, string, string> failedValidationAction)
        {
            int i = 0;
            var actualObjectValue = default(bool);
            var actualResponseValuesToArray = Check(actualContent, failedValidationAction);

            actualResponseValuesToArray.Children().ForEach(arg =>
            {
                actualObjectValue = GetJsonToken<bool>(arg.Value<JToken>(), name);

                bool result = CommonValidator.ValidateBinding(() => actualObjectValue.Should().BeTrue());

                if (!result)
                {
                    failedValidationAction(
                         $"actual object value with path [JSON].{arg.Path}.{name}: '{actualObjectValue}' ",
                         $"should Be True with expected value, ",
                         "but was in fact different!",
                         arg.Parent[i].ToString());
                }
                i++;
            });

            Reporter.Pass($"Check actual [JSON].[value].[start/end - 1/{actualResponseValuesToArray.Count}].{name}: '{actualObjectValue}' Be True", CodeBlockType.Label);
        }

        /// <summary>
        /// Tvrdi, ze hodnota je false
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazvy property objektov</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldBeFalse(
            HttpContent actualContent,
            string name,
            Action<string, string, string, string> failedValidationAction)
        {
            int i = 0;
            var actualObjectValue = default(bool);
            var actualResponseValuesToArray = Check(actualContent, failedValidationAction);

            actualResponseValuesToArray.Children().ForEach(arg =>
            {
                actualObjectValue = GetJsonToken<bool>(arg.Value<JToken>(), name);

                bool result = CommonValidator.ValidateBinding(() => actualObjectValue.Should().BeFalse());

                if (!result)
                {
                    failedValidationAction(
                         $"actual object value with path [JSON].{arg.Path}.{name}: '{actualObjectValue}' ",
                         $"should Be False with expected value, ",
                         "but was in fact different!",
                         arg.Parent[i].ToString());
                }
                i++;
            });

            Reporter.Pass($"Check actual [JSON].[value].[start/end - 1/{actualResponseValuesToArray.Count}].{name}: '{actualObjectValue}' Be False", CodeBlockType.Label);
        }

        /// <summary>
        /// Tvrdi, ze hodnota nie je null
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="failedValidationAction">Nastavenie chybovej hlasky</param>
        public static void ShouldNotBeNull(
            HttpContent actualContent,
            Action<string, string, string, string> failedValidationAction
            )
        {
            var actualResponseValuesToString = actualContent.ReadAsStringAsync().Result;

            bool result = CommonValidator.ValidateBinding(() => actualResponseValuesToString.Should().NotBeNull());

            if (!result)
            {
                failedValidationAction(
                     $"actual object value ",
                     $"should Be Not null, ",
                     "but was in fact different!",
                     string.Empty);
            }
        }

        /// <summary>
        /// Testuje, či správa o odozve HTTP obsahuje hlavičku odpovede s určitým názvom a zberom hodnoty.
        /// </summary>
        /// <param name="headers">HTTP headers.</param>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="values">Collection of values in the expected response header.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="isContentHeader">Indicates whether the header is content header.</param>
        public static void ContainingHeader(
            HttpHeaders headers,
            string name,
            IEnumerable<string> values,
            Action<string, string, string> failedValidationAction,
            bool isContentHeader = false)
        {
            ContainingHeader(headers, name, failedValidationAction, isContentHeader);
            var actualHeaderValuesWithExpectedName = GetHeaderValues(headers, name);
            var expectedValues = values.ToList();
            var actualValuesCount = actualHeaderValuesWithExpectedName.Count;
            var expectedValuesCount = expectedValues.Count;
            if (expectedValuesCount != actualValuesCount)
            {
                failedValidationAction(
                    isContentHeader ? "content headers" : "headers",
                    string.Format("to contain {0} with {1} values", name, expectedValuesCount),
                    string.Format("instead found {0}", actualValuesCount));
            }

            var sortedActualValues = actualHeaderValuesWithExpectedName.OrderBy(v => v).ToList();
            var sortedExpectedValues = expectedValues.OrderBy(v => v).ToList();

            for (int i = 0; i < sortedActualValues.Count; i++)
            {
                var actualValue = sortedActualValues[i];
                var expectedValue = sortedExpectedValues[i];
                if (actualValue != expectedValue)
                {
                    failedValidationAction(
                        isContentHeader ? "content headers" : "headers",
                        string.Format("to have {0} with {1} value", name, expectedValue),
                        "none was found");
                }
            }
        }



        /// <summary>
        /// Overenie celkového počtu hlavičiek HTTP.
        /// </summary>
        /// <param name="expectedHeaders">Expected HTTP headers.</param>
        /// <param name="actualHeaders">Actual HTTP headers.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="isContentHeaders">Indicates whether the headers are content headers.</param>
        public static void ValidateHeadersCount(
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> expectedHeaders,
            HttpHeaders actualHeaders,
            Action<string, string, string> failedValidationAction,
            bool isContentHeaders = false)
        {
            var actualHeadersCount = actualHeaders.Count();
            var expectedHeadersCount = expectedHeaders.Count();

            if (expectedHeadersCount != actualHeadersCount)
            {
                failedValidationAction(
                    isContentHeaders ? "content headers" : "headers",
                    string.Format("to be {0}", expectedHeadersCount),
                    string.Format("were in fact {0}", actualHeadersCount));
            }
        }

        /// <summary>
        /// Testuje, či sa od spomenutého obsahu správy HTTP odošle hĺbkovo rovnaký objekt ako poskytnutý.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="content">Actual HTTP content.</param>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="failedResponseModelValidationAction">Function returning exception, in case of failed response model validation.</param>
        /// <returns>The actual HTTP response model.</returns>
        public static TResponseModel WithResponseModel<TResponseModel>(
            HttpContent content,
            TResponseModel expectedModel,
            Action<string, string, string> failedValidationAction,
            Func<string, string, ResponseModelAssertionException> failedResponseModelValidationAction)
        {
            var actualModel = GetActualContentModel<TResponseModel>(
                content,
                failedResponseModelValidationAction);

            if (Reflection.AreNotDeeplyEqual(expectedModel, actualModel))
            {
                throw failedResponseModelValidationAction("be the given model", "in fact it was a different model");
            }

            return actualModel;
        }

        /// <summary>
        /// Získa aktuálny model odpovedí z obsahu HTTP.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="content">Actual HTTP content.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <returns>The actual HTTP response model.</returns>
        public static TResponseModel GetActualContentModel<TResponseModel>(
            HttpContent content,
            Func<string, string, ResponseModelAssertionException> failedValidationAction)
        {
            try
            {
                return content.ReadAsAsync<TResponseModel>().Result;
            }
            catch (UnsupportedMediaTypeException)
            {
                throw failedValidationAction(
                   string.Format("be a {0}", typeof(TResponseModel).ToFriendlyTypeName()),
                   "instead received a different model");
            }
        }

        /// <summary>
        /// Získa aktuálny model odpovedí z obsahu HTTP pomocou deserializacie
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="content">Actual HTTP content.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <returns>The actual HTTP response model.</returns>
        public static TResponseModel DeserializeActualContentModel<TResponseModel>(
            HttpContent content,
            Func<string, string, ResponseModelAssertionException> failedValidationAction)
        {
            try
            {
                var actualContentAsString = content.ReadAsStringAsync().Result;
                JObject data = JObject.Parse(actualContentAsString);
                var tempToken = data["value"][1];

                return JsonConvert.DeserializeObject<TResponseModel>(tempToken.ToString());
            }
            catch (UnsupportedMediaTypeException)
            {
                throw failedValidationAction(
                   string.Format("be a {0}", typeof(TResponseModel).ToFriendlyTypeName()),
                   "instead received a different model");
            }
        }

        /// <summary>
        /// Získa aktuálny model odpovedí z obsahu HTTP pomocou deserializacie
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="content">Actual HTTP content.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <returns>The actual HTTP response model.</returns>
        public static TResponseModel DeserializeactualContentModel<TResponseModel>(
            HttpContent content,
            Func<string, string, ResponseModelAssertionException> failedValidationAction)
        {
            try
            {
                var actualContentAsString = content.ReadAsStringAsync().Result;
                JObject data = JObject.Parse(actualContentAsString);
                var tempToken = data["value"][0];

                return JsonConvert.DeserializeObject<TResponseModel>(tempToken.ToString());
            }
            catch (UnsupportedMediaTypeException)
            {
                throw failedValidationAction(
                   string.Format("be a {0}", typeof(TResponseModel).ToFriendlyTypeName()),
                   "instead received a different model");
            }
        }

        public static void WithJProperty(
            HttpContent content,
            string key,
            string expectedValue,
            Func<string, string, ResponseModelAssertionException> failedValidationAction)
        {
            var actualContentAsString = content.ReadAsStringAsync().Result;

            var resultObjects = AllChildren(JObject.Parse(actualContentAsString))
              .First(c => c.Type == JTokenType.Array && c.Path.Contains("value"))
              .Children<JObject>();

            foreach (JObject result in resultObjects)
            {
                foreach (JProperty property in result.Properties())
                {
                    var prop = property.Where(jp => ((JProperty)jp).Name == key).Select(jp => ((JProperty)jp).Value);

                    if (expectedValue != prop.ToString())
                    {
                        failedValidationAction(
                            "property value",
                            string.Format("to be {0}", expectedValue,
                            string.Format("instead received {0}", prop.ToString())));
                    }
                }
            }
        }

        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }

        /// <summary>
        /// Testuje, či je stavový kód správy o odozve HTTP rovnaký ako kód HttpStatusCode.
        /// </summary>
        /// <param name="httpResponseMessage">HTTP response message to test.</param>
        /// <param name="statusCode">Expected status code.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidWithStatusCode(
            HttpResponseMessage httpResponseMessage,
            HttpStatusCode statusCode,
            Action<string, string, string> failedValidationAction)
        {
            var actualStatusCode = httpResponseMessage.StatusCode;
            if (actualStatusCode != statusCode)
            {               
                failedValidationAction(
                    "status code",
                    string.Format("to be {0} ({1})", (int)statusCode, statusCode),
                    string.Format("instead received {0} ({1})", (int)actualStatusCode, actualStatusCode));
            }
            Reporter.Pass("Status code: " + httpResponseMessage.StatusCode, CodeBlockType.Label);
        }

        /// <summary>
        /// Testuje, či verzia správy HTTP odpovede je rovnaká ako poskytnutá verzia.
        /// </summary>
        /// <param name="httpResponseMessage">HTTP response message to test.</param>
        /// <param name="version">Expected version.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void WithVersion(
            HttpResponseMessage httpResponseMessage,
            Version version,
            Action<string, string, string> failedValidationAction)
        {
            var actualVersion = httpResponseMessage.Version;
            if (actualVersion != version)
            {
                failedValidationAction(
                    "version",
                    string.Format("to be {0}", version),
                    string.Format("instead received {0}", actualVersion));
            }
        }

        /// <summary>
        /// Testuje, či fráza odôvodnenia správy HTTP je rovnaká ako fráza poskytnutá dôvodom ako reťazec.
        /// </summary>
        /// <param name="httpResponseMessage">HTTP response message to test.</param>
        /// <param name="reasonPhrase">Expected reason phrase as string.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidWithReasonPhrase(
            HttpResponseMessage httpResponseMessage,
            string reasonPhrase,
            Action<string, string, string> failedValidationAction)
        {
            var actualReasonPhrase = httpResponseMessage.ReasonPhrase;
            if (actualReasonPhrase != reasonPhrase)
            {
                failedValidationAction(
                    "reason phrase",
                    string.Format("to be '{0}'", reasonPhrase),
                    string.Format("instead received '{0}'", actualReasonPhrase));
            }
        }

        /// <summary>
        /// Testuje, či hlásenie odozvy HTTP vráti stavový kód úspešnosti medzi 200 a 299.
        /// </summary>
        /// <param name="httpResponseMessage">HTTP response message to test.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void WithSuccessStatusCode(HttpResponseMessage httpResponseMessage, Action<string, string, string> failedValidationAction)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                failedValidationAction(
                    "status code",
                    string.Format("to be between 200 and 299"),
                    string.Format("it was not"));
            }
        }

        private static JArray GetJsonContent(string content)
        { 
            var containerToken = JToken.Parse(content);

            if (containerToken.Count() == 0)
            {
                throw new VerificationException("Data not found!");
            }

            if (containerToken.Type == JTokenType.Object)
            {
                var o = JObject.Parse(content);
                return o["value"].Value<JArray>();
            }

            if (containerToken.Type == JTokenType.Array)
            {
                var o = JArray.Parse(content);
                return o.Value<JArray>();
            }

            return containerToken["value"].Value<JArray>();
        }

        private static T GetJsonToken<T>(JToken content, string attributeName)
        {
            try
            {
                content.SelectToken(attributeName).Value<T>();
            }
            catch (ArgumentException)
            {
                throw new VerificationException($"Nenašiel sa definovaný object path '{attributeName}' v JSON {content}");
            }

            return content.SelectToken(attributeName).Value<T>();
        }

        private static T GetJsonTokenNested<T>(JToken content, IEnumerable<string> attributeNames)
        {
            try
            {
                content.SelectToken(attributeNames.ElementAt(0)).Select(m => (T)m.SelectToken(attributeNames.ElementAt(1)).Value<T>());
            }
            catch (ArgumentException)
            {
                throw new VerificationException($"Nenašiel sa definovaný object path v kolekcii '{attributeNames.ElementAt(0)}.{attributeNames.ElementAt(1)}' v JSON {content}");
            }

            return content.SelectToken(attributeNames.ElementAt(0)).Select(m => (T)m.SelectToken(attributeNames.ElementAt(1)).Value<T>()).First();
        }

        private static IList<string> GetHeaderValues(HttpHeaders headers, string name)
        {
            return headers.First(h => h.Key == name).Value.ToList();
        }       
    }
}
