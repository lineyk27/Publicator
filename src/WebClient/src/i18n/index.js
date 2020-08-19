import i18n from "i18next";
import LanguageDetector from "i18next-browser-languagedetector";
import en from "./locales/en.js";
import uk from "./locales/uk.js";

const options = {
    interpolation:{
        escapeValue : false
    },
    lng: "en",
    debug: true,
    resources:{
        en: en,
        uk: uk
    },
};

i18n
    .use(LanguageDetector)
    .init(options);

export default i18n;
