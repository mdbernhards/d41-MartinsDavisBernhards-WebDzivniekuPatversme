namespace WebDzivniekuPatversme.Models.StaticModels
{
    public static class ValidationErrorMessages
    {
        public const string StringLength = "{0} ir par garu!";

        public const string RequiredM = "{0} ir obligāts!";

        public const string RequiredF = "{0} ir obligāta!";

        public const string DateValidation = "{0} nevar būt nākotnē!";

        public const string DoesNotExistInDb = "{0} datubāzē neeksistē!";

        public const string FileType = "Šis faila tips nav atļauts!";

        public const string WeightRange = "Svaram jābūt vismaz {1} Kg un mazākam par {2} Kg!";

        public const string MaxFileSize = "Maksimālais faila lielums ir 6 MB!";

        public const string NotValid = "Ievadītais {0} nav derīgs!";

        public const string CapacityRange = "Kapacitāte nevar būt mazāka par {1} un lielāka par {2}!";

        public const string NewPasswordStringLenght = "Jaunajai parolei jābūt garumā no {2} līdz {1}!";

        public const string ComparePassword = "Paroles nesakrīt!";

        public const string VerificationCodeStringLenght = "Verifikācijas kodam jābūt garumā no {2} līdz {1}!";

        public const string AlreadyExists = "{0} jau datubāzē eksistē!";
    }
}