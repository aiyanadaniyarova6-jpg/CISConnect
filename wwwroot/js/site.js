/* =============================================================
   CIS Connect — Tropical Editorial JS
   Sticky-shrink navbar · Parallax hero · Scroll reveals
   Marquee · Horizontal pin · FAQ accordion · Cursor · Magnetic
   ============================================================= */

(function () {
  'use strict';

  /* ── Cursor ────────────────────────────────────────────────── */
  var cursor = document.getElementById('cursor');
  if (cursor && matchMedia('(hover: hover) and (pointer: fine)').matches) {
    var cx = -100, cy = -100;
    document.addEventListener('mousemove', function (e) {
      cx = e.clientX; cy = e.clientY;
      cursor.style.left = cx + 'px';
      cursor.style.top  = cy + 'px';
    });
    document.querySelectorAll('a, button, .post, .uni-row, .faq-item, .save-post-btn').forEach(function (el) {
      el.addEventListener('mouseenter', function () { cursor.classList.add('expand'); });
      el.addEventListener('mouseleave', function () { cursor.classList.remove('expand'); });
    });
  }

  /* ── Navbar shrink on scroll ───────────────────────────────── */
  var nav = document.querySelector('.site-nav');
  function updateNav() {
    if (!nav) return;
    nav.classList.toggle('shrunk', window.scrollY > 80);
  }
  window.addEventListener('scroll', updateNav, { passive: true });
  updateNav();

  /* ── Mobile navigation drawer ─────────────────────────────── */
  var mobileToggle = document.querySelector('.nav-mobile-toggle');
  var mobileDrawer = document.getElementById('mobileNavDrawer');

  function closeMobileMenu() {
    if (!nav || !mobileToggle) return;
    nav.classList.remove('mobile-open');
    mobileToggle.setAttribute('aria-expanded', 'false');
  }

  if (nav && mobileToggle && mobileDrawer) {
    mobileToggle.addEventListener('click', function () {
      var isOpen = nav.classList.toggle('mobile-open');
      mobileToggle.setAttribute('aria-expanded', isOpen ? 'true' : 'false');
    });

    mobileDrawer.querySelectorAll('a').forEach(function (link) {
      link.addEventListener('click', closeMobileMenu);
    });

    window.addEventListener('resize', function () {
      if (window.innerWidth > 760) closeMobileMenu();
    });
  }

  /* ── Saved posts via localStorage ─────────────────────────── */
  var savedKey = 'cisconnect.savedPosts.v1';

  function getSavedPosts() {
    try {
      return JSON.parse(localStorage.getItem(savedKey) || '[]');
    } catch {
      return [];
    }
  }

  function setSavedPosts(posts) {
    localStorage.setItem(savedKey, JSON.stringify(posts));
  }

  function getSavePayload(button) {
    var source = button.closest('[data-save-card]') || button;
    return {
      id: source.dataset.saveId || button.dataset.saveId || '',
      title: source.dataset.saveTitle || button.dataset.saveTitle || '',
      summary: source.dataset.saveSummary || button.dataset.saveSummary || '',
      category: source.dataset.saveCategory || button.dataset.saveCategory || 'Guide',
      date: source.dataset.saveDate || button.dataset.saveDate || '',
      url: source.dataset.saveUrl || button.dataset.saveUrl || '#'
    };
  }

  function isSaved(id) {
    return getSavedPosts().some(function (post) { return post.id === id; });
  }

  function updateSaveButtons() {
    document.querySelectorAll('[data-save-button]').forEach(function (button) {
      var payload = getSavePayload(button);
      var active = payload.id && isSaved(payload.id);
      button.classList.toggle('saved', active);
      button.setAttribute('aria-pressed', active ? 'true' : 'false');
      button.textContent = button.classList.contains('detail')
        ? (active ? '♥ Saved' : '♡ Save')
        : (active ? '♥' : '♡');
    });
  }

  document.querySelectorAll('[data-save-button]').forEach(function (button) {
    button.addEventListener('click', function (event) {
      event.preventDefault();
      event.stopPropagation();

      var payload = getSavePayload(button);
      if (!payload.id) return;

      var posts = getSavedPosts();
      var existing = posts.findIndex(function (post) { return post.id === payload.id; });

      if (existing >= 0) {
        posts.splice(existing, 1);
      } else {
        posts.unshift(payload);
      }

      setSavedPosts(posts);
      updateSaveButtons();
      renderSavedPage();
    });
  });

  function renderSavedPage() {
    var grid = document.getElementById('savedGrid');
    var empty = document.getElementById('savedEmpty');
    var count = document.getElementById('savedCount');
    if (!grid) return;

    var posts = getSavedPosts();
    grid.innerHTML = posts.map(function (post) {
      return [
        '<article class="saved-card">',
        '<div><span class="post-badge">' + escapeHtml(post.category) + '</span>',
        '<h2>' + escapeHtml(post.title) + '</h2>',
        '<p>' + escapeHtml(post.summary) + '</p></div>',
        '<div class="saved-card-actions">',
        '<span class="mono">' + escapeHtml(post.date || 'Saved post') + '</span>',
        '<a class="soft-button" href="' + escapeAttribute(post.url || '#') + '">Open</a>',
        '</div></article>'
      ].join('');
    }).join('');

    if (empty) empty.style.display = posts.length ? 'none' : '';
    if (count) count.textContent = posts.length + ' saved post' + (posts.length !== 1 ? 's' : '');
  }

  function escapeHtml(value) {
    return String(value || '')
      .replace(/&/g, '&amp;')
      .replace(/</g, '&lt;')
      .replace(/>/g, '&gt;')
      .replace(/"/g, '&quot;')
      .replace(/'/g, '&#039;');
  }

  function escapeAttribute(value) {
    return escapeHtml(value).replace(/`/g, '&#096;');
  }

  var clearSavedBtn = document.getElementById('clearSavedBtn');
  if (clearSavedBtn) {
    clearSavedBtn.addEventListener('click', function () {
      setSavedPosts([]);
      updateSaveButtons();
      renderSavedPage();
    });
  }

  updateSaveButtons();
  renderSavedPage();

  /* ── Checklist local progress ─────────────────────────────── */
  var checklistKey = 'cisconnect.checklist.v1';
  var checklistItems = Array.from(document.querySelectorAll('[data-checklist-item]'));

  function getChecklistState() {
    try {
      return JSON.parse(localStorage.getItem(checklistKey) || '{}');
    } catch {
      return {};
    }
  }

  function setChecklistState(state) {
    localStorage.setItem(checklistKey, JSON.stringify(state));
  }

  function updateChecklistProgress() {
    if (!checklistItems.length) return;
    var state = getChecklistState();
    var done = 0;

    checklistItems.forEach(function (item) {
      item.checked = Boolean(state[item.value]);
      if (item.checked) done++;
    });

    var percent = Math.round((done / checklistItems.length) * 100);
    var text = document.getElementById('checklistProgressText');
    var fill = document.getElementById('checklistProgressFill');
    if (text) text.textContent = percent + '% complete';
    if (fill) fill.style.width = percent + '%';
  }

  checklistItems.forEach(function (item) {
    item.addEventListener('change', function () {
      var state = getChecklistState();
      state[item.value] = item.checked;
      setChecklistState(state);
      updateChecklistProgress();
    });
  });

  var resetChecklistBtn = document.getElementById('resetChecklistBtn');
  if (resetChecklistBtn) {
    resetChecklistBtn.addEventListener('click', function () {
      setChecklistState({});
      updateChecklistProgress();
    });
  }

  updateChecklistProgress();

  /* ── Country support filter ───────────────────────────────── */
  var countrySearch = document.getElementById('countrySupportSearch');
  var countryCards = Array.from(document.querySelectorAll('[data-country-card]'));

  if (countrySearch && countryCards.length) {
    countrySearch.addEventListener('input', function () {
      var query = countrySearch.value.trim().toLowerCase();
      countryCards.forEach(function (card) {
        var match = !query || (card.dataset.country || '').includes(query);
        card.style.display = match ? '' : 'none';
      });
    });
  }

  /* ── Dropdown: hover + click lock with delay ─────────────────── */
  document.querySelectorAll('.nav-dropdown-wrap').forEach(function (wrap) {
    var menu = wrap.querySelector('.nav-dropdown-menu');
    var trigger = wrap.querySelector('[data-dropdown-trigger]');
    var timer = null;
    var clickLocked = false;

    function show() {
      clearTimeout(timer);
      document.querySelectorAll('.nav-dropdown-wrap.open').forEach(function (other) {
        if (other !== wrap) {
          other.classList.remove('open');
          other.dataset.locked = 'false';
          var otherTrigger = other.querySelector('[data-dropdown-trigger]');
          if (otherTrigger) otherTrigger.setAttribute('aria-expanded', 'false');
        }
      });
      wrap.classList.add('open');
      if (trigger) trigger.setAttribute('aria-expanded', 'true');
      if (menu) {
        menu.style.animation = 'none';
        menu.offsetHeight; // reflow
        menu.style.animation = 'dropFade 0.3s var(--ease-out-expo)';
      }
    }

    function hide() {
      if (wrap.dataset.locked === 'true') return;
      timer = setTimeout(function () {
        wrap.classList.remove('open');
        if (trigger) trigger.setAttribute('aria-expanded', 'false');
      }, 260);
    }

    wrap.addEventListener('mouseenter', show);
    wrap.addEventListener('mouseleave', hide);
    if (menu) {
      menu.addEventListener('mouseenter', function () { clearTimeout(timer); });
      menu.addEventListener('mouseleave', hide);
    }

    if (trigger) {
      trigger.addEventListener('click', function (event) {
        event.preventDefault();
        event.stopPropagation();

        var willOpen = !wrap.classList.contains('open') || !clickLocked;
        document.querySelectorAll('.nav-dropdown-wrap.open').forEach(function (other) {
          if (other !== wrap) {
            other.classList.remove('open');
            other.dataset.locked = 'false';
            var otherTrigger = other.querySelector('[data-dropdown-trigger]');
            if (otherTrigger) otherTrigger.setAttribute('aria-expanded', 'false');
          }
        });

        clickLocked = willOpen;
        wrap.dataset.locked = clickLocked ? 'true' : 'false';
        if (willOpen) show();
        else {
          wrap.classList.remove('open');
          trigger.setAttribute('aria-expanded', 'false');
        }
      });
    }

    if (menu) {
      menu.querySelectorAll('a').forEach(function (link) {
        link.addEventListener('click', function () {
          clickLocked = false;
          wrap.dataset.locked = 'false';
          wrap.classList.remove('open');
          if (trigger) trigger.setAttribute('aria-expanded', 'false');
        });
      });
    }
  });

  document.addEventListener('click', function () {
    document.querySelectorAll('.nav-dropdown-wrap.open').forEach(function (wrap) {
      wrap.classList.remove('open');
      wrap.dataset.locked = 'false';
      var trigger = wrap.querySelector('[data-dropdown-trigger]');
      if (trigger) trigger.setAttribute('aria-expanded', 'false');
    });
  });

  /* ── Hero: fire in-view immediately (first section is always visible) */
  var hero = document.querySelector('.hero');
  if (hero) {
    // Small delay so CSS transitions have a starting point to animate FROM
    setTimeout(function () { hero.classList.add('in-view', 'is-visible'); }, 60);
  }

  /* ── Hero parallax flower ──────────────────────────────────── */
  var heroFlower = document.querySelector('.hero-flower');
  function updateParallax() {
    if (!heroFlower) return;
    var y = window.scrollY;
    heroFlower.style.transform = 'translateY(calc(-50% + ' + (y * 0.18) + 'px)) rotate(-12deg)';
  }
  window.addEventListener('scroll', updateParallax, { passive: true });

  /* ── Guide visual: eased scroll float inside the card ─────── */
  var guideVisuals = Array.from(document.querySelectorAll('[data-guide-float]')).map(function (visual) {
    return {
      el: visual,
      current: 0,
      target: 0
    };
  });
  var guideFloatRunning = false;

  function setGuideVisualTargets() {
    if (!guideVisuals.length) return;

    var viewportHeight = window.innerHeight || document.documentElement.clientHeight;
    guideVisuals.forEach(function (item) {
      var rect = item.el.getBoundingClientRect();
      var center = rect.top + rect.height / 2;
      var distanceFromCenter = (center - viewportHeight / 2) / viewportHeight;
      item.target = Math.max(-14, Math.min(14, distanceFromCenter * -24));
    });

    if (!guideFloatRunning) {
      guideFloatRunning = true;
      requestAnimationFrame(animateGuideVisuals);
    }
  }

  function animateGuideVisuals() {
    var keepGoing = false;

    guideVisuals.forEach(function (item) {
      item.current += (item.target - item.current) * 0.09;

      if (Math.abs(item.target - item.current) > 0.08) {
        keepGoing = true;
      } else {
        item.current = item.target;
      }

      item.el.style.setProperty('--guide-scroll-y', item.current.toFixed(2) + 'px');
    });

    if (keepGoing) {
      requestAnimationFrame(animateGuideVisuals);
    } else {
      guideFloatRunning = false;
    }
  }

  if (guideVisuals.length) {
    window.addEventListener('scroll', setGuideVisualTargets, { passive: true });
    window.addEventListener('resize', setGuideVisualTargets);
    setGuideVisualTargets();
  }

  /* ── Scroll-triggered reveals ──────────────────────────────── */
  var revealSelectors = [
    '[data-reveal]',
    '.fade-up',
    '.in-view-trigger',
    '.section-head',
    '.story-inner',
    '.section',
  ];

  var observer = new IntersectionObserver(function (entries) {
    entries.forEach(function (entry) {
      if (entry.isIntersecting) {
        entry.target.classList.add('in-view', 'is-visible');
        observer.unobserve(entry.target);
      }
    });
  }, { threshold: 0.12, rootMargin: '0px 0px -60px 0px' });

  revealSelectors.forEach(function (sel) {
    document.querySelectorAll(sel).forEach(function (el) {
      observer.observe(el);
    });
  });

  /* ── Word reveal ───────────────────────────────────────────── */
  document.querySelectorAll('.word-reveal').forEach(function (el) {
    var words = el.textContent.split(/\s+/).filter(Boolean);
    el.innerHTML = words.map(function (w, i) {
      return '<span class="w" style="--d:' + (i * 60) + 'ms">' + w + '</span>';
    }).join(' ');
  });

  /* ── FAQ accordion ─────────────────────────────────────────── */
  document.querySelectorAll('.faq-item').forEach(function (item) {
    var q = item.querySelector('.faq-q');
    if (!q) return;
    q.addEventListener('click', function () {
      var isOpen = item.classList.contains('open');
      // close all
      document.querySelectorAll('.faq-item.open').forEach(function (other) {
        other.classList.remove('open');
      });
      if (!isOpen) item.classList.add('open');
    });
  });

  /* ── FAQ search + category filter ─────────────────────────── */
  var faqSearch    = document.getElementById('faqSearchInput');
  var faqClear     = document.getElementById('faqClearBtn');
  var faqCountEl   = document.getElementById('faqCount');
  var faqItems     = Array.from(document.querySelectorAll('.faq-item'));
  var faqCatChips  = document.querySelectorAll('[data-faq-cat]');
  var activeCat    = 'all';

  function highlight(text, query) {
    if (!query) return text;
    var re = new RegExp('(' + query.replace(/[.*+?^${}()|[\]\\]/g, '\\$&') + ')', 'gi');
    return text.replace(re, '<mark>$1</mark>');
  }

  function filterFAQ() {
    var query = faqSearch ? faqSearch.value.trim().toLowerCase() : '';
    var visible = 0;
    faqItems.forEach(function (item) {
      var q   = item.querySelector('.faq-q h4');
      var a   = item.querySelector('.faq-a p');
      var cat = (item.dataset.faqCategory || '').toLowerCase();
      var qText = q ? q.dataset.original || q.textContent : '';
      var aText = a ? a.dataset.original || a.textContent : '';

      // store originals once
      if (q && !q.dataset.original) q.dataset.original = q.textContent;
      if (a && !a.dataset.original) a.dataset.original = a.textContent;

      var catMatch = activeCat === 'all' || cat === activeCat;
      var textMatch = !query || qText.toLowerCase().includes(query) || aText.toLowerCase().includes(query);
      var show = catMatch && textMatch;

      item.style.display = show ? '' : 'none';
      if (show) {
        visible++;
        if (q) q.innerHTML = highlight(qText, query);
        if (a) a.innerHTML = highlight(aText, query);
      }
    });

    if (faqCountEl) {
      faqCountEl.textContent = visible + ' result' + (visible !== 1 ? 's' : '');
    }

    // show/hide empty state
    var empty = document.getElementById('faqEmptyFiltered');
    if (empty) empty.style.display = visible === 0 ? '' : 'none';
  }

  if (faqSearch) {
    faqSearch.addEventListener('input', filterFAQ);
  }
  if (faqClear) {
    faqClear.addEventListener('click', function () {
      if (faqSearch) faqSearch.value = '';
      activeCat = 'all';
      faqCatChips.forEach(function (c) { c.classList.toggle('active', c.dataset.faqCat === 'all'); });
      filterFAQ();
    });
  }
  faqCatChips.forEach(function (chip) {
    chip.addEventListener('click', function () {
      activeCat = chip.dataset.faqCat;
      faqCatChips.forEach(function (c) { c.classList.remove('active'); });
      chip.classList.add('active');
      filterFAQ();
    });
  });

  /* ── Filter chips (Updates feed) ───────────────────────────── */
  var feedChips  = document.querySelectorAll('[data-filter-chip]');
  var feedPosts  = document.querySelectorAll('[data-update-type]');

  feedChips.forEach(function (chip) {
    chip.addEventListener('click', function () {
      var val = chip.dataset.filterValue || 'All';
      feedChips.forEach(function (c) { c.classList.remove('active'); c.setAttribute('aria-pressed', 'false'); });
      chip.classList.add('active');
      chip.setAttribute('aria-pressed', 'true');

      feedPosts.forEach(function (post) {
        var match = val === 'All' || (post.dataset.updateType || '') === val;
        post.style.display = match ? '' : 'none';
      });
    });
  });

  /* ── Guide filter chips (university or country, depending on section) ─── */
  document.querySelectorAll('[data-guide-filter-chip]').forEach(function (chip) {
    chip.addEventListener('click', function () {
      var bar = chip.closest('.guide-filter-bar');
      var guideArea = bar ? bar.parentElement : document;
      var chips = bar ? Array.from(bar.querySelectorAll('[data-guide-filter-chip]')) : [];
      var cards = Array.from(guideArea.querySelectorAll('[data-guide-tag]'));
      var emptyState = guideArea.querySelector('[data-guide-empty-filter]');
      var selected = chip.dataset.guideFilterValue || 'All';
      var visibleCount = 0;

      chips.forEach(function (item) {
        var active = item === chip;
        item.classList.toggle('active', active);
        item.setAttribute('aria-pressed', active ? 'true' : 'false');
      });

      cards.forEach(function (card) {
        var match = selected === 'All' || (card.dataset.guideTag || '') === selected;
        card.classList.toggle('is-filter-hidden', !match);
        if (match) visibleCount++;
      });

      if (emptyState) {
        emptyState.classList.toggle('is-visible', visibleCount === 0);
      }
    });
  });

  /* ── Horizontal pin (Menu sections) ────────────────────────── */
  var hScrollOuter = document.querySelector('.h-scroll-outer');
  var hScrollPin   = document.querySelector('.h-scroll-sticky');
  var hScrollTrack = document.querySelector('.h-scroll-track');
  var hScrollBar   = document.querySelector('.h-scroll-progress .bar');
  var hScrollLabel = document.querySelector('.h-scroll-progress .label');

  if (hScrollOuter && hScrollPin && hScrollTrack) {
    // Only run on non-mobile
    if (!matchMedia('(max-width: 760px)').matches) {
      var sections = Array.from(hScrollTrack.querySelectorAll('.menu-card'));
      var trackWidth = 0;

      function calcTrackWidth() {
        trackWidth = hScrollTrack.scrollWidth - window.innerWidth;
        hScrollOuter.style.height = (hScrollPin.offsetHeight + trackWidth) + 'px';
      }
      calcTrackWidth();
      window.addEventListener('resize', calcTrackWidth);

      function updateHScroll() {
        var rect = hScrollOuter.getBoundingClientRect();
        var scrolled = -rect.top;
        if (scrolled < 0 || scrolled > rect.height - window.innerHeight) return;
        var progress = Math.max(0, Math.min(1, scrolled / trackWidth));
        hScrollTrack.style.transform = 'translateX(' + (-scrolled) + 'px)';
        if (hScrollBar) hScrollBar.style.setProperty('--p', (progress * 100).toFixed(1) + '%');
      }
      window.addEventListener('scroll', updateHScroll, { passive: true });
      updateHScroll();
    }
  }

  /* ── University tabs ───────────────────────────────────────── */
  document.querySelectorAll('.uni-detail-card').forEach(function (card) {
    var tabs  = card.querySelectorAll('.uni-tab');
    var panes = card.querySelectorAll('.uni-tab-pane');
    tabs.forEach(function (tab, i) {
      tab.addEventListener('click', function () {
        tabs.forEach(function (t) { t.classList.remove('active'); });
        panes.forEach(function (p) { p.classList.remove('active'); });
        tab.classList.add('active');
        var targetPane = tab.dataset.tab
          ? card.querySelector('#tab-' + tab.dataset.tab)
          : panes[i];
        if (targetPane) targetPane.classList.add('active');
      });
    });
  });

  /* ── Compare universities ─────────────────────────────────── */
  var compareTool = document.querySelector('[data-compare-tool]');
  var compareDataScript = document.getElementById('compareUniversityData');
  if (compareTool && compareDataScript) {
    var compareItems = [];
    try {
      compareItems = JSON.parse(compareDataScript.textContent || '[]');
    } catch (error) {
      compareItems = [];
    }

    var leftSelect = compareTool.querySelector('[data-compare-select="left"]');
    var rightSelect = compareTool.querySelector('[data-compare-select="right"]');
    var compareById = compareItems.reduce(function (map, item) {
      map[String(item.id)] = item;
      return map;
    }, {});

    function setDefaultComparison() {
      if (!leftSelect || !rightSelect || compareItems.length === 0) return;
      leftSelect.value = String(compareItems[0].id);
      rightSelect.value = String((compareItems[1] || compareItems[0]).id);
    }

    function updateColumn(side, university) {
      var column = compareTool.querySelector('[data-compare-column="' + side + '"]');
      if (!column || !university) return;

      column.querySelectorAll('[data-field]').forEach(function (field) {
        field.textContent = university[field.dataset.field] || '';
      });

      column.querySelectorAll('[data-field-src]').forEach(function (image) {
        var imageUrl = university[image.dataset.fieldSrc] || '';
        image.src = imageUrl;
        image.hidden = !imageUrl;
      });

      column.querySelectorAll('[data-field-alt]').forEach(function (image) {
        image.alt = university[image.dataset.fieldAlt] || '';
      });

      column.querySelectorAll('[data-field-link]').forEach(function (link) {
        link.href = university[link.dataset.fieldLink] || '#';
      });

      compareTool.querySelectorAll('[data-compare-value="' + side + '"]').forEach(function (value) {
        value.textContent = university[value.dataset.field] || 'Information is being prepared.';
      });
    }

    function syncDisabledOptions() {
      if (!leftSelect || !rightSelect) return;
      Array.from(leftSelect.options).forEach(function (option) {
        option.disabled = option.value === rightSelect.value;
      });
      Array.from(rightSelect.options).forEach(function (option) {
        option.disabled = option.value === leftSelect.value;
      });
    }

    function renderCompare() {
      if (!leftSelect || !rightSelect) return;

      if (leftSelect.value === rightSelect.value && compareItems.length > 1) {
        var replacement = compareItems.find(function (item) {
          return String(item.id) !== leftSelect.value;
        });
        if (replacement) rightSelect.value = String(replacement.id);
      }

      compareTool.classList.add('is-switching');
      window.setTimeout(function () {
        updateColumn('left', compareById[leftSelect.value]);
        updateColumn('right', compareById[rightSelect.value]);
        syncDisabledOptions();
        compareTool.classList.remove('is-switching');
      }, 120);
    }

    setDefaultComparison();
    renderCompare();
    if (leftSelect) leftSelect.addEventListener('change', renderCompare);
    if (rightSelect) rightSelect.addEventListener('change', renderCompare);
  }

  /* ── Magnetic buttons ──────────────────────────────────────── */
  document.querySelectorAll('.magnet').forEach(function (el) {
    el.addEventListener('mousemove', function (e) {
      var rect = el.getBoundingClientRect();
      var dx = e.clientX - rect.left - rect.width  / 2;
      var dy = e.clientY - rect.top  - rect.height / 2;
      el.style.transform = 'translate(' + dx * 0.35 + 'px, ' + dy * 0.35 + 'px)';
    });
    el.addEventListener('mouseleave', function () {
      el.style.transform = '';
    });
  });

  /* ── Read-time progress bar ────────────────────────────────── */
  var readBar     = document.getElementById('readingProgress');
  var readContent = document.querySelector('[data-reading-content]');
  if (readBar && readContent) {
    readBar.style.display = 'block';
    window.addEventListener('scroll', function () {
      var top  = readContent.getBoundingClientRect().top + window.scrollY;
      var h    = readContent.offsetHeight - window.innerHeight;
      var prog = Math.max(0, Math.min(1, (window.scrollY - top) / (h || 1)));
      readBar.style.width = (prog * 100).toFixed(1) + '%';
    }, { passive: true });
  }

  /* ── Marquee duplicate ─────────────────────────────────────── */
  var track = document.querySelector('.marquee-track');
  if (track) {
    var inner = track.innerHTML;
    track.innerHTML = inner + inner; // duplicate for seamless loop
  }

  /* ── Share button ───────────────────────────────────────────── */
  var shareBtn   = document.getElementById('shareBtn');
  var shareLbl   = document.getElementById('shareBtnLabel');
  if (shareBtn) {
    shareBtn.addEventListener('click', function () {
      var url = window.location.href;
      if (navigator.share) {
        navigator.share({ url: url }).catch(function () {});
      } else if (navigator.clipboard) {
        navigator.clipboard.writeText(url).then(function () {
          shareBtn.classList.add('copied');
          if (shareLbl) shareLbl.textContent = 'Copied!';
          setTimeout(function () {
            shareBtn.classList.remove('copied');
            if (shareLbl) shareLbl.textContent = 'Share';
          }, 2000);
        });
      }
    });
  }

  /* ── Relative timestamps ────────────────────────────────────── */
  (function () {
    var now = new Date();
    document.querySelectorAll('[data-rel-date]').forEach(function (el) {
      var raw = el.getAttribute('data-rel-date');
      if (!raw) return;
      var d = new Date(raw);
      if (isNaN(d.getTime())) return;
      var diffMs  = now - d;
      var diffDay = Math.floor(diffMs / 86400000);
      var text;
      if (diffDay === 0)       text = 'Today';
      else if (diffDay === 1)  text = 'Yesterday';
      else if (diffDay < 7)   text = diffDay + ' days ago';
      else if (diffDay < 14)  text = '1 week ago';
      else if (diffDay < 30)  text = Math.floor(diffDay / 7) + ' weeks ago';
      else if (diffDay < 60)  text = '1 month ago';
      else if (diffDay < 365) text = Math.floor(diffDay / 30) + ' months ago';
      else                    text = el.textContent; // keep original for old dates
      el.textContent = text;
    });
  }());

  /* ── Live search ────────────────────────────────────────────── */
  (function () {
    var searchInput   = document.getElementById('searchInput');
    var liveResults   = document.getElementById('liveResults');
    var serverResults = document.getElementById('serverResults');
    var suggestions   = document.getElementById('searchSuggestions');
    if (!searchInput || !liveResults || !serverResults) return;

    var debounceTimer;

    function escHtml(str) {
      return String(str)
        .replace(/&/g, '&amp;').replace(/</g, '&lt;')
        .replace(/>/g, '&gt;').replace(/"/g, '&quot;');
    }

    function renderLive(items, query) {
      if (!items.length) {
        liveResults.innerHTML =
          '<div class="empty-state">' +
          '<svg class="empty-icon-svg" width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5"><circle cx="11" cy="11" r="8"/><path d="m21 21-4.35-4.35"/></svg>' +
          '<h2>No results for “' + escHtml(query) + '”</h2>' +
          '<p>Try a shorter keyword: visa, arrival, documents, community.</p>' +
          '</div>';
        return;
      }
      var html = '<div class="search-result-head"><span class="mono">' + items.length + ' result' + (items.length !== 1 ? 's' : '') + '</span></div>';
      html += '<div class="saved-grid search-results">';
      items.forEach(function (item) {
        var url = item.url ? item.url : '/post/' + escHtml(item.publicId);
        html +=
          '<article class="saved-card">' +
          '<div>' +
          '<div style="display:flex;gap:8px;align-items:center;margin-bottom:8px;flex-wrap:wrap;">' +
          '<span class="post-badge">' + escHtml(item.category) + '</span>' +
          '<span class="search-source-tag">' + escHtml(item.sourceLabel) + '</span>' +
          '</div>' +
          '<h2>' + escHtml(item.title) + '</h2>' +
          '<p>' + escHtml(item.summary || '') + '</p>' +
          '</div>' +
          '<div class="saved-card-actions">' +
          '<span class="mono">' + escHtml(item.publishedAt) + '</span>' +
          '<a class="soft-button" href="' + url + '">Open</a>' +
          '</div>' +
          '</article>';
      });
      html += '</div>';
      liveResults.innerHTML = html;
    }

    function doLiveSearch(q) {
      if (q.length < 2) {
        liveResults.style.display = 'none';
        serverResults.style.display = '';
        if (suggestions) suggestions.style.display = '';
        return;
      }
      if (suggestions) suggestions.style.display = 'none';
      fetch('/Search/Live?q=' + encodeURIComponent(q))
        .then(function (r) { return r.json(); })
        .then(function (data) {
          renderLive(data, q);
          liveResults.style.display = '';
          serverResults.style.display = 'none';
        })
        .catch(function () {
          liveResults.style.display = 'none';
          serverResults.style.display = '';
        });
    }

    searchInput.addEventListener('input', function () {
      clearTimeout(debounceTimer);
      var q = searchInput.value.trim();
      debounceTimer = setTimeout(function () { doLiveSearch(q); }, 280);
    });

    // Suggestion chips
    document.querySelectorAll('[data-search-suggestion]').forEach(function (chip) {
      chip.addEventListener('click', function () {
        searchInput.value = chip.dataset.searchSuggestion;
        searchInput.dispatchEvent(new Event('input'));
        searchInput.focus();
      });
    });

    // Trigger live search on load if there's already a query value
    if (searchInput.value.trim().length >= 2) {
      doLiveSearch(searchInput.value.trim());
    }
  }());

  /* ── GA4 analytics events ──────────────────────────────────── */
  (function () {
    function ga(event, params) {
      if (typeof gtag === 'function' && window.__ga4Id) {
        gtag('event', event, params || {});
      }
    }

    // Search submitted
    document.querySelectorAll('form[action*="Search"]').forEach(function (form) {
      form.addEventListener('submit', function () {
        var q = form.querySelector('input[name="q"]');
        if (q && q.value.trim()) ga('search', { search_term: q.value.trim() });
      });
    });

    // Post saved / unsaved
    document.addEventListener('click', function (e) {
      var btn = e.target.closest('[data-save-button]');
      if (!btn) return;
      var card = btn.closest('[data-save-card]');
      var title = card ? card.dataset.saveTitle : 'unknown';
      var saved = btn.getAttribute('aria-pressed') !== 'true';
      ga(saved ? 'save_post' : 'unsave_post', { post_title: title });
    });

    // University comparison opened
    var compareLink = document.querySelector('a[href*="Compare"]');
    if (compareLink) {
      compareLink.addEventListener('click', function () {
        ga('open_university_compare');
      });
    }

    // Checklist item toggled
    document.addEventListener('change', function (e) {
      if (e.target.matches('[data-checklist-item]')) {
        ga('checklist_toggle', { checked: e.target.checked });
      }
    });

    // Share button used
    document.querySelectorAll('[data-share-btn]').forEach(function (btn) {
      btn.addEventListener('click', function () {
        ga('share', { method: navigator.share ? 'native' : 'clipboard', content_type: 'post' });
      });
    });
  }());

  /* ── Scroll progress bar ───────────────────────────────────────────────── */
  var scrollProgressBar = document.getElementById('scrollProgressBar');
  if (scrollProgressBar) {
    window.addEventListener('scroll', function () {
      var docH = document.documentElement.scrollHeight - window.innerHeight;
      var pct  = docH > 0 ? (window.scrollY / docH * 100).toFixed(1) : 0;
      scrollProgressBar.style.width = pct + '%';
    }, { passive: true });
  }

  /* ── Cursor trail ring ─────────────────────────────────────────────────── */
  var cursorTrail = document.getElementById('cursorTrail');
  if (cursorTrail && matchMedia('(hover: hover) and (pointer: fine)').matches) {
    var trailTx = -100, trailTy = -100;
    var trailX  = -100, trailY  = -100;
    var trailRunning = false;

    document.addEventListener('mousemove', function (e) {
      trailTx = e.clientX; trailTy = e.clientY;
      if (!trailRunning) {
        trailRunning = true;
        requestAnimationFrame(animateTrail);
      }
    });

    function animateTrail() {
      trailX += (trailTx - trailX) * 0.11;
      trailY += (trailTy - trailY) * 0.11;
      cursorTrail.style.left = trailX.toFixed(1) + 'px';
      cursorTrail.style.top  = trailY.toFixed(1) + 'px';
      if (Math.abs(trailTx - trailX) > 0.3 || Math.abs(trailTy - trailY) > 0.3) {
        requestAnimationFrame(animateTrail);
      } else {
        trailRunning = false;
      }
    }
  }

  /* ── Stat counter (hero) ───────────────────────────────────────────────── */
  var statsSection = document.querySelector('.stats');
  if (statsSection) {
    var statsCounted = false;
    var statsObserver = new IntersectionObserver(function (entries) {
      if (!entries[0].isIntersecting || statsCounted) return;
      statsCounted = true;
      statsSection.querySelectorAll('.num').forEach(function (el) {
        var target = parseInt(el.textContent, 10);
        if (isNaN(target) || target === 0) return;
        var duration = 1200;
        var startTime = null;
        function step(ts) {
          if (!startTime) startTime = ts;
          var progress = Math.min((ts - startTime) / duration, 1);
          var eased = 1 - Math.pow(1 - progress, 3);
          el.textContent = Math.round(target * eased);
          if (progress < 1) { requestAnimationFrame(step); }
          else { el.textContent = target; }
        }
        requestAnimationFrame(step);
      });
    }, { threshold: 0.6 });
    statsObserver.observe(statsSection);
  }

  /* ── Page transitions (home-bound only) ────────────────────────────────── */
  var routeOverlay = document.getElementById('routeOverlay');
  if (routeOverlay) {
    var transitioning = false;

    // Returns true if a pathname points to the home/updates feed
    function isHomePath(p) {
      var n = (p || '').split('?')[0].split('#')[0].toLowerCase().replace(/\/$/, '');
      return n === '' || n === '/updates' || n === '/updates/index';
    }

    // On load: if we arrived via our transition, slide the overlay away
    if (sessionStorage.getItem('cis-page-transition')) {
      sessionStorage.removeItem('cis-page-transition');
      routeOverlay.classList.add('entering');
      requestAnimationFrame(function () {
        requestAnimationFrame(function () {
          routeOverlay.classList.remove('entering');
          routeOverlay.classList.add('leaving');
          setTimeout(function () { routeOverlay.classList.remove('leaving'); }, 800);
        });
      });
    }

    // Only fire when navigating TO home FROM a different page
    document.addEventListener('click', function (e) {
      if (transitioning) return;
      var link = e.target.closest('a[href]');
      if (!link) return;
      var href = link.getAttribute('href');
      if (!href ||
          href.charAt(0) === '#' ||
          href.indexOf('mailto:') === 0 ||
          href.indexOf('tel:') === 0 ||
          link.target === '_blank' ||
          link.hasAttribute('download') ||
          /^(https?:)?\/\//.test(href)) return;

      // Gate: current page must NOT be home, destination must BE home
      if (isHomePath(window.location.pathname) || !isHomePath(href.split('?')[0])) return;

      e.preventDefault();
      transitioning = true;
      sessionStorage.setItem('cis-page-transition', '1');
      routeOverlay.classList.remove('leaving');
      routeOverlay.classList.add('entering');
      setTimeout(function () { window.location.href = href; }, 660);
    });
  }

  /* ── Pagination: restore scroll position to feed section ───────────────── */
  (function () {
    // On load: if URL contains ?page=, scroll feed into view (not the hero)
    if (/[?&]page=/.test(window.location.search) && window.location.hash !== '#feed') {
      var feedEl = document.getElementById('feed');
      if (feedEl) {
        setTimeout(function () {
          feedEl.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }, 120);
      }
    }
  }());

  /* ── Hero: petals burst in all directions on scroll ────────────────────── */
  (function () {
    var heroEl    = document.querySelector('.hero');
    var flowerEl  = document.querySelector('.hero-flower');
    if (!heroEl || !flowerEl) return;

    // dx/dy are direction multipliers × fw (flower width). Negative dy = upward.
    // 8 petals spread across all 360° like an explosion
    var PETAL_CONFIGS = [
      { dx: -2.2, dy: -2.0, rot: -210, size: 0.38, delay: 0.00 }, // upper-left
      { dx:  0.2, dy: -2.8, rot:  165, size: 0.31, delay: 0.04 }, // straight up
      { dx:  2.0, dy: -1.6, rot: -145, size: 0.35, delay: 0.08 }, // upper-right
      { dx:  2.6, dy:  0.4, rot:  230, size: 0.27, delay: 0.12 }, // right
      { dx:  1.4, dy:  2.5, rot: -255, size: 0.41, delay: 0.16 }, // lower-right
      { dx: -0.3, dy:  3.0, rot:  185, size: 0.29, delay: 0.20 }, // straight down
      { dx: -2.0, dy:  1.8, rot: -175, size: 0.37, delay: 0.24 }, // lower-left
      { dx: -2.8, dy: -0.2, rot:  215, size: 0.32, delay: 0.28 }, // left
    ];

    // overlay div inside hero, behind content — overflow visible so upward petals show
    var petalLayer = document.createElement('div');
    petalLayer.style.cssText = 'position:absolute;inset:0;pointer-events:none;z-index:1;overflow:visible;';
    heroEl.insertBefore(petalLayer, heroEl.firstChild);

    function getFlowerCenter() {
      var heroRect   = heroEl.getBoundingClientRect();
      var flowerRect = flowerEl.getBoundingClientRect();
      return {
        x: flowerRect.left + flowerRect.width  * 0.5 - heroRect.left,
        y: flowerRect.top  + flowerRect.height * 0.5 - heroRect.top + window.scrollY,
      };
    }

    var petals = PETAL_CONFIGS.map(function (cfg, i) {
      var fw   = flowerEl.offsetWidth || 400;
      var pxSz = Math.round(fw * cfg.size);
      var svg  = document.createElementNS('http://www.w3.org/2000/svg', 'svg');
      svg.setAttribute('viewBox', '-55 -150 110 155');
      svg.style.cssText = [
        'position:absolute',
        'width:'  + pxSz + 'px',
        'height:' + Math.round(pxSz * 1.4) + 'px',
        'opacity:0',
        'pointer-events:none',
        'will-change:transform,opacity',
      ].join(';');

      var pathEl = document.createElementNS('http://www.w3.org/2000/svg', 'path');
      pathEl.setAttribute('d', 'M 0 0 C -36 -28 -44 -78 -22 -118 C -10 -140 10 -140 22 -118 C 44 -78 36 -28 0 0 Z');
      pathEl.setAttribute('fill', '#c2410c');
      // slight opacity variation per petal
      pathEl.setAttribute('opacity', (0.75 + i * 0.05).toFixed(2));
      svg.appendChild(pathEl);
      petalLayer.appendChild(svg);
      return { el: svg, cfg: cfg };
    });

    function placePetals() {
      var fw  = flowerEl.offsetWidth || 400;
      var ctr = getFlowerCenter();
      petals.forEach(function (p) {
        var pxSz = Math.round(fw * p.cfg.size);
        p.el.style.width  = pxSz + 'px';
        p.el.style.height = Math.round(pxSz * 1.4) + 'px';
        // scatter start positions slightly within the flower centre
        var jitterX = (Math.random() - 0.5) * fw * 0.3;
        var jitterY = (Math.random() - 0.5) * fw * 0.3;
        p.el.style.left = Math.round(ctr.x - pxSz / 2 + jitterX) + 'px';
        p.el.style.top  = Math.round(ctr.y - pxSz * 0.7 + jitterY) + 'px';
        p.startX = parseFloat(p.el.style.left);
        p.startY = parseFloat(p.el.style.top);
        p.fw = fw;
      });
    }

    placePetals();
    window.addEventListener('resize', placePetals);

    window.addEventListener('scroll', function () {
      /* Complete petal burst by the time user scrolls ~40% of one viewport height */
      var progress = Math.min(window.scrollY / (window.innerHeight * 0.42), 1);

      petals.forEach(function (p) {
        /* compress delays so burst is tighter */
        var delay = p.cfg.delay * 0.35;
        var raw = (progress - delay) / (1 - delay + 0.001);
        var t   = Math.max(0, Math.min(1, raw));
        // smoothstep ease
        var e   = t * t * (3 - 2 * t);

        var dx  = e * p.cfg.dx * p.fw * 0.55;
        var dy  = e * p.cfg.dy * p.fw * 0.40;
        var rot = e * p.cfg.rot;
        var op  = t < 0.06 ? t / 0.06 : (t > 0.76 ? Math.max(0, (1 - t) / 0.24) : 1);

        p.el.style.transform = 'translate(' + dx.toFixed(1) + 'px,' + dy.toFixed(1) + 'px) rotate(' + rot.toFixed(1) + 'deg)';
        p.el.style.opacity   = (op * 0.88).toFixed(3);
      });
    }, { passive: true });
  }());

  /* ── Journey section: floating animated SVG paths (cursor-reactive) ─────── */
  (function () {
    var section = document.getElementById('journey');
    if (!section) return;

    var NS = 'http://www.w3.org/2000/svg';

    // Inject the flowing-dash keyframe once
    if (!document.getElementById('cisJourneyAnim')) {
      var ks = document.createElement('style');
      ks.id = 'cisJourneyAnim';
      // 1050 = 21 repeats of (30+20)=50 dash unit → perfectly seamless loop
      ks.textContent = [
        '@keyframes cisFlowDash {',
        '  from { stroke-dashoffset: 1050; }',
        '  to   { stroke-dashoffset: 0; }',
        '}',
        '@media (prefers-reduced-motion: reduce) {',
        '  .journey-path-anim { animation: none !important; }',
        '}',
      ].join('');
      document.head.appendChild(ks);
    }

    // SVG overlay — paths render inside it, cursor moves control points via JS
    var svg = document.createElementNS(NS, 'svg');
    svg.setAttribute('aria-hidden', 'true');
    svg.setAttribute('preserveAspectRatio', 'none');
    svg.style.cssText = 'position:absolute;inset:0;width:100%;height:100%;pointer-events:none;z-index:0;overflow:visible;';
    section.insertBefore(svg, section.firstChild);

    var container = section.querySelector('.container');
    if (container) container.style.position = 'relative';

    // 10 path definitions — varied y positions, curve directions, visual weight
    // cpFlip: which way the S-curve bends; cpMag: how wide the curve is
    var ROUTES = [
      { sy: 0.06, ey: 0.94, cpFlip:  1, cpMag: 0.32, sw: 1.8, op: 0.20, dur: 16, delay:  0   },
      { sy: 0.18, ey: 0.82, cpFlip: -1, cpMag: 0.28, sw: 2.2, op: 0.24, dur: 20, delay: -4   },
      { sy: 0.30, ey: 0.70, cpFlip:  1, cpMag: 0.22, sw: 1.5, op: 0.18, dur: 14, delay: -8   },
      { sy: 0.42, ey: 0.58, cpFlip: -1, cpMag: 0.35, sw: 2.5, op: 0.28, dur: 22, delay: -2   },
      { sy: 0.50, ey: 0.50, cpFlip:  1, cpMag: 0.18, sw: 1.2, op: 0.16, dur: 18, delay: -11  },
      { sy: 0.62, ey: 0.38, cpFlip: -1, cpMag: 0.30, sw: 2.0, op: 0.22, dur: 24, delay: -6   },
      { sy: 0.74, ey: 0.26, cpFlip:  1, cpMag: 0.26, sw: 1.6, op: 0.19, dur: 15, delay: -14  },
      { sy: 0.85, ey: 0.15, cpFlip: -1, cpMag: 0.38, sw: 2.3, op: 0.25, dur: 19, delay: -3   },
      { sy: 0.14, ey: 0.78, cpFlip:  1, cpMag: 0.42, sw: 1.4, op: 0.17, dur: 21, delay: -9   },
      { sy: 0.66, ey: 0.22, cpFlip: -1, cpMag: 0.24, sw: 1.9, op: 0.21, dur: 17, delay: -7   },
    ];

    var W = 0, H = 0;
    var mx = 0.5, my = 0.5, tmx = 0.5, tmy = 0.5;
    var pathEls = [];
    var rafId = null;

    function buildD(r) {
      var cursorX = (mx - 0.5) * W * 0.14;
      var cursorY = (my - 0.5) * H * 0.22;
      var midY = (r.sy + r.ey) / 2 * H;
      var cp1x = W * 0.27 + cursorX;
      var cp1y = midY + r.cpFlip * H * r.cpMag + cursorY;
      var cp2x = W * 0.73 + cursorX;
      var cp2y = midY - r.cpFlip * H * r.cpMag - cursorY;
      return 'M-40,' + (r.sy * H).toFixed(1) +
             ' C' + cp1x.toFixed(1) + ',' + cp1y.toFixed(1) +
             ' ' + cp2x.toFixed(1) + ',' + cp2y.toFixed(1) +
             ' ' + (W + 40).toFixed(1) + ',' + (r.ey * H).toFixed(1);
    }

    function rebuildPaths() {
      while (svg.firstChild) svg.removeChild(svg.firstChild);
      pathEls = [];
      ROUTES.forEach(function (r) {
        var p = document.createElementNS(NS, 'path');
        p.setAttribute('fill', 'none');
        p.setAttribute('stroke', '#c2410c');
        p.setAttribute('stroke-width', r.sw.toFixed(1));
        p.setAttribute('stroke-opacity', r.op.toFixed(2));
        p.setAttribute('stroke-dasharray', '30 20');
        p.setAttribute('stroke-linecap', 'round');
        p.setAttribute('d', buildD(r));
        p.classList.add('journey-path-anim');
        p.style.animation = 'cisFlowDash ' + r.dur + 's linear ' + r.delay + 's infinite';
        svg.appendChild(p);
        pathEls.push({ el: p, r: r });
      });
    }

    function updateDs() {
      pathEls.forEach(function (item) {
        item.el.setAttribute('d', buildD(item.r));
      });
    }

    function resize() {
      W = section.offsetWidth;
      H = section.offsetHeight;
      rebuildPaths();
    }

    // Cursor tracking — lerp mx/my then update SVG path shapes
    function lerpTick() {
      var dx = tmx - mx, dy = tmy - my;
      mx += dx * 0.055;
      my += dy * 0.055;
      updateDs();
      if (Math.abs(dx) > 0.001 || Math.abs(dy) > 0.001) {
        rafId = requestAnimationFrame(lerpTick);
      } else {
        rafId = null;
      }
    }

    if (matchMedia('(hover: hover) and (pointer: fine)').matches) {
      section.addEventListener('mousemove', function (e) {
        var rect = section.getBoundingClientRect();
        tmx = (e.clientX - rect.left) / rect.width;
        tmy = (e.clientY - rect.top)  / rect.height;
        if (!rafId) rafId = requestAnimationFrame(lerpTick);
      });
      section.addEventListener('mouseleave', function () {
        tmx = 0.5; tmy = 0.5;
        if (!rafId) rafId = requestAnimationFrame(lerpTick);
      });
    }

    window.addEventListener('resize', resize);
    resize();
  }());

  /* ── Story section: bubbles rising from below ───────────────────────────── */
  (function () {
    var section = document.querySelector('.story-section');
    if (!section) return;

    var canvas = document.createElement('canvas');
    canvas.setAttribute('aria-hidden', 'true');
    canvas.style.cssText = 'position:absolute;inset:0;width:100%;height:100%;pointer-events:none;z-index:0;';
    section.insertBefore(canvas, section.firstChild);

    var container = section.querySelector('.container');
    if (container) container.style.position = 'relative';

    var ctx = canvas.getContext('2d');
    var W = 0, H = 0;
    var bubbles = [];
    var loopActive = false;
    var frameCount = 0;

    function newBubble() {
      var r = 5 + Math.random() * 22;
      return {
        x:        r + Math.random() * Math.max(1, W - 2 * r),
        y:        H + r + Math.random() * 40,
        r:        r,
        vy:       0.35 + Math.random() * 0.55,
        opacity:  0.12 + Math.random() * 0.18,
        wobble:   Math.random() * Math.PI * 2,
        wSpeed:   0.018 + Math.random() * 0.022,
      };
    }

    function tick() {
      if (!loopActive) return;
      frameCount++;

      ctx.clearRect(0, 0, W, H);

      // spawn rate: roughly 1 bubble per 25 frames
      if (frameCount % 25 === 0) bubbles.push(newBubble());

      bubbles = bubbles.filter(function (b) { return b.y + b.r > -20; });

      bubbles.forEach(function (b) {
        b.y      -= b.vy;
        b.wobble += b.wSpeed;
        var bx = b.x + Math.sin(b.wobble) * 4;

        var fadeIn  = Math.min(1, (H - b.y - b.r) / 80);   // fade in from bottom
        var fadeOut = Math.min(1, Math.max(0, (b.y - b.r) / 100)); // pop near top
        var op = b.opacity * Math.min(fadeIn, fadeOut);
        if (op <= 0) return;

        // outer ring
        ctx.beginPath();
        ctx.arc(bx, b.y, b.r, 0, Math.PI * 2);
        ctx.strokeStyle = 'rgba(194,65,12,' + op.toFixed(3) + ')';
        ctx.lineWidth   = 1;
        ctx.stroke();

        // inner fill (very subtle)
        ctx.beginPath();
        ctx.arc(bx, b.y, b.r, 0, Math.PI * 2);
        ctx.fillStyle   = 'rgba(194,65,12,' + (op * 0.08).toFixed(3) + ')';
        ctx.fill();

        // specular highlight dot
        ctx.beginPath();
        ctx.arc(bx - b.r * 0.32, b.y - b.r * 0.32, b.r * 0.18, 0, Math.PI * 2);
        ctx.fillStyle = 'rgba(255,255,255,' + (op * 0.5).toFixed(3) + ')';
        ctx.fill();
      });

      requestAnimationFrame(tick);
    }

    function resize() {
      W = canvas.width  = section.offsetWidth;
      H = canvas.height = section.offsetHeight;
    }

    var io = new IntersectionObserver(function (entries) {
      loopActive = entries[0].isIntersecting;
      if (loopActive) requestAnimationFrame(tick);
    }, { threshold: 0.05 });

    window.addEventListener('resize', resize);
    resize();
    io.observe(section);
  }());

  /* ── Immersive scroll-expand (KL cinematic) ─────────────────── */
  (function () {
    var outer   = document.getElementById('immOuter');
    if (!outer) return;

    var mediaEl = document.getElementById('immMedia');
    var iframe  = mediaEl ? mediaEl.querySelector('iframe') : null;
    var titleEl = document.getElementById('immTitle');
    var wordL   = outer.querySelector('.imm-left');
    var wordR   = outer.querySelector('.imm-right');
    var tagEl   = document.getElementById('immTagline');
    var nudgeEl = document.getElementById('immNudge');

    var W, H, mobile;

    function measure() {
      W = window.innerWidth;
      H = window.innerHeight;
      mobile = W < 640;
    }
    measure();

    /* Cover-fill iframe: always 16% overscan, centered via 50% + negative margin.
       This is the most reliable way to hide any letterbox bars from YouTube. */
    function coverIframe(cW, cH) {
      if (!iframe) return;
      var ratio    = 16 / 9;
      var OVER     = 1.25;
      var iW, iH;
      if (cW / cH >= ratio) {
        iW = cW * OVER;
        iH = iW / ratio;
      } else {
        iH = cH * OVER;
        iW = iH * ratio;
      }
      iframe.style.width      = iW.toFixed(0) + 'px';
      iframe.style.height     = iH.toFixed(0) + 'px';
      iframe.style.left       = '50%';
      iframe.style.top        = '50%';
      iframe.style.marginLeft = (-iW / 2).toFixed(0) + 'px';
      iframe.style.marginTop  = (-iH / 2).toFixed(0) + 'px';
    }

    function ease(t) { return t * t * (3 - 2 * t); } /* smoothstep */

    function update() {
      var rect  = outer.getBoundingClientRect();
      var outerH = outer.offsetHeight;
      /* Animation starts only when section is ~50% visible (user can
         see the portrait state first), then expands over 0.9H of scroll. */
      var p = Math.max(0, Math.min(1, (H * 0.5 - rect.top) / (H * 0.9)));
      var e = ease(p);

      /* Portrait start → landscape fullscreen end */
      var sw = mobile ? 170 : 240;   /* start width  */
      var sh = mobile ? 300 : 420;   /* start height (portrait) */
      var mW = sw + e * (W  - sw);
      var mH = sh + e * (H  - sh);
      var mR = (1 - e) * 18;

      mediaEl.style.width        = mW.toFixed(0) + 'px';
      mediaEl.style.height       = mH.toFixed(0) + 'px';
      mediaEl.style.borderRadius = mR.toFixed(1) + 'px';

      coverIframe(mW, mH);

      /* Title: split words apart, fade out in first half of scroll */
      var split   = e * (mobile ? 14 : 22);
      var titleOp = Math.max(0, 1 - p * 3);
      if (wordL)  wordL.style.transform  = 'translateX(-' + split.toFixed(2) + 'vw)';
      if (wordR)  wordR.style.transform  = 'translateX('  + split.toFixed(2) + 'vw)';
      if (titleEl) titleEl.style.opacity = titleOp.toFixed(3);

      /* nudge fades immediately */
      if (nudgeEl) nudgeEl.style.opacity = Math.max(0, 1 - p * 8).toFixed(3);

      /* tagline fades in near end */
      if (tagEl) tagEl.classList.toggle('visible', p > 0.80);
    }

    window.addEventListener('scroll', update, { passive: true });
    window.addEventListener('resize', function () { measure(); update(); });
    update();
  }());

  /* ── About page: ContainerScroll tilt ───────────────────────────── */
  (function () {
    var outer = document.getElementById('aboutTiltOuter');
    var card  = document.getElementById('aboutTiltCard');
    if (!outer || !card) return;

    function smoothstep(t) { return t * t * (3 - 2 * t); }

    function update() {
      var rect = outer.getBoundingClientRect();
      var H    = window.innerHeight;
      /* p=0 when card enters viewport from below, p=1 when card top reaches 25% from top */
      var p = Math.max(0, Math.min(1, (H - rect.top) / (H * 1.1)));
      var e = smoothstep(p);

      /* Mirror ContainerScroll:
         - rotateX 24→0  (card tilts forward to face viewer)
         - scale 1.08→1  (starts enlarged, settles to normal)
         - translateY 90→0  (rises into position)
         - opacity 0→1  */
      var rotX = 24 * (1 - e);
      var sc   = 1.08 - 0.08 * e;
      var ty   = 90  * (1 - e);
      card.style.transform = 'rotateX(' + rotX.toFixed(2) + 'deg) scale(' + sc.toFixed(3) + ') translateY(' + ty.toFixed(1) + 'px)';
      card.style.opacity   = Math.min(1, p * 2.5).toFixed(3);
    }

    /* initial hidden state */
    card.style.transform = 'rotateX(24deg) scale(1.08) translateY(90px)';
    card.style.opacity   = '0';

    window.addEventListener('scroll', update, { passive: true });
    window.addEventListener('resize', update);
    update();
  }());

  /* ── Feed: show-more on mobile ───────────────────────────────────── */
  (function () {
    var btn  = document.getElementById('feedShowMoreBtn');
    var feed = document.querySelector('.feed');
    if (!btn || !feed) return;
    btn.addEventListener('click', function () {
      feed.classList.add('expanded');
      document.getElementById('feedShowMore').style.display = 'none';
    });
  }());

  /* ── Embassy grid: collapsible on mobile ──────────────────────────── */
  (function () {
    var toggle = document.getElementById('countryHubToggle');
    var grid   = document.getElementById('countrySupportGrid');
    var label  = toggle && toggle.querySelector('.country-hub-toggle-label');
    if (!toggle || !grid) return;
    toggle.addEventListener('click', function () {
      var expanded = toggle.getAttribute('aria-expanded') === 'true';
      toggle.setAttribute('aria-expanded', expanded ? 'false' : 'true');
      grid.classList.toggle('collapsed', expanded);
      if (label) label.textContent = expanded ? 'Show' : 'Hide';
    });
    /* Collapse by default on mobile */
    if (window.innerWidth <= 760) {
      toggle.setAttribute('aria-expanded', 'false');
      grid.classList.add('collapsed');
      if (label) label.textContent = 'Show';
    }
  }());

  /* ── Display Cards — click to bring card forward ─────────────────── */
  (function () {
    var cards = document.querySelectorAll('.dc-card');
    if (!cards.length) return;

    function deactivateAll() {
      cards.forEach(function (c) { c.classList.remove('dc-card--active'); });
    }

    cards.forEach(function (card) {
      card.addEventListener('click', function (e) {
        e.stopPropagation();
        var isActive = card.classList.contains('dc-card--active');
        deactivateAll();
        if (!isActive) card.classList.add('dc-card--active');
      });
    });

    /* Click outside stack to dismiss */
    document.addEventListener('click', deactivateAll);
  }());

  /* ── Footer country cycle ─────────────────────────────────────────── */
  (function () {
    var el   = document.getElementById('footerCycleWord');
    var wrap = el && el.parentElement;
    if (!el || !wrap) return;

    var words   = ['CIS', 'Malaysia', 'Kazakhstan', 'Kyrgyzstan', 'Uzbekistan', 'Tajikistan'];
    var current = 0;

    /* Size the wrap to fit the tallest word on first paint */
    el.classList.add('visible');

    function next() {
      current = (current + 1) % words.length;

      /* 1. Exit current word upward */
      el.classList.remove('visible');
      el.classList.add('exit');

      setTimeout(function () {
        /* 2. Instantly jump new word in from below (no transition) */
        el.textContent = words[current];
        el.classList.remove('exit');
        el.classList.add('enter');

        /* Force reflow so the browser registers the class before animating */
        void el.offsetHeight;

        /* 3. Slide into view */
        el.classList.remove('enter');
        el.classList.add('visible');
      }, 480); /* matches exit opacity duration */
    }

    setInterval(next, 2200);
  }());

})();
