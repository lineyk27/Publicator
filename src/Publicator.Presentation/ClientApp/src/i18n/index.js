import i18n from "i18next";
import LanguageDetector from "i18next-browser-languagedetector";
import {en, uk} from "./locales";

const options = {
    interpolation:{
        escapeValue : false
    },
    debug: true,
    resources:{
        en: {
            common: en.en
        },
        uk:{
            common: uk.uk
        }
    },
    fallbackLng: "en",
    ns: ["common"],
    defaultNS: "common",
    react: {
        wait: false,
        bindI18n: "languageChanged loaded",
        bingStore: "added removed",
        nsMode: "default"
    },
};

i18n
    .use(LanguageDetector)
    .init(options)
    .changeLanguage('uk', (err, t) => {
        if(err) return console.log("Translation was not loaded.", err);
});

export default i18n;
