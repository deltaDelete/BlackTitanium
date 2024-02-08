import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
      "@shadcn": path.resolve(__dirname, "./@shadcn")
    }
  },
  server: {
    proxy: {
      '^/api': {
        target: 'http://localhost:5212',
        secure: false
      }
    },
    port: 5211,
  }
})
