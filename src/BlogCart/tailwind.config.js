/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
    theme: {
        screens: {
            'sm': '640px',
            'md': '1024px', // modified breakpoint for medium screens
            'lg': '1024px',
            'xl': '1280px',
            '2xl': '1536px',
        },
        extend: {
            margin: {
                'sm': '0 0 10px 0', //added
            },
            colors: {
                //'Primary': '##594ae2ff',   //these colors match mudblazor naming conventions
                //'PrimaryLighten': '#2b6cb0',
                //'GrayLight': '#F5F5F',
                //'Surface': '#ffffffff', //Card bg color
                'medicare-blue': '#00447A',
                'darken-medicare-blue': '#003057', // a darker shade of Medicare Blue
                'custom-gray': '#D3D3D3',
            },
        },
    },
    variants: {},
    plugins: [],
}