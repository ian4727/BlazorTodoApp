/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["../Components/**/*.{razor,cs}"],
  theme: {
    extend: {
      boxShadow: {
        'outline-red': '0 0 10px #b83f45',
      },
    },
  },
  plugins: [],
}

