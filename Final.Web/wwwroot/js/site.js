// StayFinder — progressive enhancements. Pages work fully without JS.
(function () {
    "use strict";

    document.documentElement.classList.add("js");

    // Navbar: elevate with a shadow once the page is scrolled.
    var nav = document.querySelector(".app-nav");
    if (nav) {
        var onScroll = function () {
            nav.classList.toggle("is-scrolled", window.scrollY > 8);
        };
        onScroll();
        window.addEventListener("scroll", onScroll, { passive: true });
    }

    // Cards: fade-up as they enter the viewport.
    if ("IntersectionObserver" in window) {
        var revealed = document.querySelectorAll(".reveal");
        if (revealed.length) {
            var io = new IntersectionObserver(function (entries) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        entry.target.classList.add("is-visible");
                        io.unobserve(entry.target);
                    }
                });
            }, { threshold: 0.08 });
            revealed.forEach(function (el) { io.observe(el); });
        }
    } else {
        document.querySelectorAll(".reveal").forEach(function (el) {
            el.classList.add("is-visible");
        });
    }
})();
