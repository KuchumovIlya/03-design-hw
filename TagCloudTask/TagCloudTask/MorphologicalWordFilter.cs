using System.Linq;
using Newtonsoft.Json;

namespace TagCloudTask
{
    public class MorphologicalWordFilter : IWordFilter
    {
        private readonly IMorphologicalParser morphologicalParser;

        //See https://tech.yandex.ru/mystem/doc/grammemes-values-docpage/#parts to learn more.
        private readonly string[] notInterestingPartsOfSpeech = {
            "APRO",
            "COM",
            "CONJ",
            "INTJ",
            "NUM",
            "PART",
            "PR",
            "SPRO",
            "ADVPRO"
        };

        public MorphologicalWordFilter(IMorphologicalParser morphologicalParser)
        {
            this.morphologicalParser = morphologicalParser;
        }

        public bool IsInterestingWord(string word)
        {
            var formatedMorphologicalDataInJson = morphologicalParser.GetFormatedMorphologicalDataInJson(word);
            dynamic morphologicData = JsonConvert.DeserializeObject(formatedMorphologicalDataInJson);

            if (morphologicData.analysis.Count == 0)
                return false;

            var gr = morphologicData.analysis[0].gr.ToString();
            return notInterestingPartsOfSpeech.All(possiblePart => !gr.StartsWith(possiblePart));
        }
    }
}
