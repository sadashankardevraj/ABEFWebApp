(() => {
  document.addEventListener("click", (event) => {
    const toggle = event.target.closest("[data-password-toggle]");
    if (!toggle) {
      return;
    }

    const wrap = toggle.closest(".auth-password-wrap");
    const input = wrap?.querySelector("input");
    const icon = toggle.querySelector("i");
    if (!input || !icon) {
      return;
    }

    const showing = input.type === "text";
    input.type = showing ? "password" : "text";
    icon.classList.toggle("bi-eye", showing);
    icon.classList.toggle("bi-eye-slash", !showing);
    toggle.setAttribute("aria-label", showing ? "Show password" : "Hide password");
  });
})();
