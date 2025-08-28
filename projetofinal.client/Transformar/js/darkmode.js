const toggleBtn = document.getElementById('toggleDarkMode');
const toggleBtn_mobile = document.getElementById('toggleDarkMode-mobile');

function applyTheme() {
    // Se ainda não existir preferencia salva, detecta a do sistema
    if (localStorage.getItem('darkMode') === null) {
        const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
        localStorage.setItem('darkMode', prefersDark);
    }

    // Usa a preferência salva
    let isDark = localStorage.getItem('darkMode') === 'true';
    document.body.classList.toggle('dark-mode', isDark);
    toggleBtn.innerText = isDark ? 'Dark Mode' : 'Light Mode';
    toggleBtn_mobile.innerText = toggleBtn.innerText;

    console.log(`Dark mode is ${isDark ? 'enabled' : 'disabled'}.`);    
}

// Botão desktop
toggleBtn.addEventListener('click', () => {
    let isDark = document.body.classList.toggle('dark-mode');
    localStorage.setItem('darkMode', isDark);
    toggleBtn.innerText = !isDark ? 'Light Mode' : 'Dark Mode';
    toggleBtn_mobile.innerText = toggleBtn.innerText;
});

// Botão mobile
toggleBtn_mobile.addEventListener('click', () => {
    let isDark = document.body.classList.toggle('dark-mode');
    localStorage.setItem('darkMode', isDark);
    toggleBtn_mobile.innerText = !isDark ? 'Light Mode' : 'Dark Mode';
    toggleBtn.innerText = toggleBtn_mobile.innerText;
});

// Aplica tema ao carregar
applyTheme();



