/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
    theme: {
        screens: {
            'sm': '640px',
            'md': '960px', // modified breakpoint for medium screens
            'lg': '1024px',
            'xl': '1280px',
            '2xl': '1536px',
        },
        extend: {
            margin: {
                'sm': '0 0 10px 0', //added
            },
            colors: {
                'Primary': '#1a4f7a',   //these colors match mudblazor naming conventions
                'PrimaryLighten': '#2b6cb0',
                'GrayLight': '#F5F5F',
                'Surface': '#ffffffff', //Card bg color
            },
        },
    },
    variants: {},
    plugins: [],
}