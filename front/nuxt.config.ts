// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
    devtools: { enabled: true },
    css: ["~/assets/main.css"],
    postcss: {
        plugins: {
            tailwindcss: {},
            autoprefixer: {},
        },
    },
    typescript: {
        typeCheck: true,
    },
    runtimeConfig: {
        public: {
            API_ENDPOINT: process.env.API_ENDPOINT,
        },
    },
});
