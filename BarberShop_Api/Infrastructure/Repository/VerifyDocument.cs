namespace BarberShop_Api.Infrastructure.Repository
{
    public class VerifyDocument
    {
        public static bool Verify(string document)
        {
            int totalFirst = 0;
            int totalSecond = 0;

            document = new string(document.Where(char.IsDigit).ToArray());

            if (document.Length == 11)
            {

                for (var i = 0; i < 9; i++)
                {
                    int number = (int)char.GetNumericValue(document[i]);
                    totalFirst += (10 - i) * number;
                    totalSecond += (11 - i) * number;
                }

                int resultFirst = totalFirst * 10 % 11;

                resultFirst = resultFirst == 10 ? 0 : resultFirst;

                if (resultFirst != (int)char.GetNumericValue(document[9]))
                {
                    return false;
                }

                totalSecond += resultFirst * 2;

                int secondResult = totalSecond * 10 % 11;

                secondResult = secondResult == 10 ? 0 : secondResult;

                return secondResult == (int)char.GetNumericValue(document[10]);
            }

            if (document.Length == 14)
            {
                int[] weightFirst = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
                int[] weightSecond = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

                for (int i = 0; i < 12; i++)
                {
                    int number = (int)char.GetNumericValue(document[i]);
                    totalFirst += weightFirst[i] * number;
                }

                int resultFirst = totalFirst % 11;
                resultFirst = resultFirst < 2 ? 0 : 11 - resultFirst;

                if (resultFirst != (int)char.GetNumericValue(document[12]))
                {
                    return false;
                }

                for (int i = 0; i < 13; i++)
                {
                    int number = (int)char.GetNumericValue(document[i]);
                    totalSecond += weightSecond[i] * number;
                }

                int resultSecond = totalSecond % 11;
                resultSecond = resultSecond < 2 ? 0 : 11 - resultSecond;

                return resultSecond == (int)char.GetNumericValue(document[13]);
            }


            return false;
        }
    }
}
