using Newtonsoft.Json;

namespace TagCloudTask
{
    public class MorphologicalWordNormalizer : IWordNormalizer
    {
        private readonly IMorphologicalParser morphologicalParser;

        public MorphologicalWordNormalizer(IMorphologicalParser morphologicalParser)
        {
            this.morphologicalParser = morphologicalParser;
        }

        public string NormalizeWord(string word)
        {
            var formatedMorphologicalDataInJson = morphologicalParser.GetFormatedMorphologicalDataInJson(word);
            dynamic morphologicData = JsonConvert.DeserializeObject(formatedMorphologicalDataInJson);

            return morphologicData.analysis.Count == 0 ?
                word : morphologicData.analysis[0].lex.ToString();
        }
    }
}
