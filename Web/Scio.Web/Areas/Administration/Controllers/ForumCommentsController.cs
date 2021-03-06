﻿// <auto-generated />
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Scio.Data;
using Scio.Data.Models;

namespace Scio.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class ForumCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForumCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administration/ForumComments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ForumComments.Include(f => f.Author).Include(f => f.Parent).Include(f => f.Post);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/ForumComments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumComment = await _context.ForumComments
                .Include(f => f.Author)
                .Include(f => f.Parent)
                .Include(f => f.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumComment == null)
            {
                return NotFound();
            }

            return View(forumComment);
        }

        // GET: Administration/ForumComments/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ParentId"] = new SelectList(_context.ForumComments, "Id", "Id");
            ViewData["PostId"] = new SelectList(_context.ForumPosts, "Id", "Id");
            return View();
        }

        // POST: Administration/ForumComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Body,AuthorId,PostId,ParentId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] ForumComment forumComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forumComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", forumComment.AuthorId);
            ViewData["ParentId"] = new SelectList(_context.ForumComments, "Id", "Id", forumComment.ParentId);
            ViewData["PostId"] = new SelectList(_context.ForumPosts, "Id", "Id", forumComment.PostId);
            return View(forumComment);
        }

        // GET: Administration/ForumComments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumComment = await _context.ForumComments.FindAsync(id);
            if (forumComment == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", forumComment.AuthorId);
            ViewData["ParentId"] = new SelectList(_context.ForumComments, "Id", "Id", forumComment.ParentId);
            ViewData["PostId"] = new SelectList(_context.ForumPosts, "Id", "Id", forumComment.PostId);
            return View(forumComment);
        }

        // POST: Administration/ForumComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Body,AuthorId,PostId,ParentId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] ForumComment forumComment)
        {
            if (id != forumComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumCommentExists(forumComment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", forumComment.AuthorId);
            ViewData["ParentId"] = new SelectList(_context.ForumComments, "Id", "Id", forumComment.ParentId);
            ViewData["PostId"] = new SelectList(_context.ForumPosts, "Id", "Id", forumComment.PostId);
            return View(forumComment);
        }

        // GET: Administration/ForumComments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumComment = await _context.ForumComments
                .Include(f => f.Author)
                .Include(f => f.Parent)
                .Include(f => f.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumComment == null)
            {
                return NotFound();
            }

            return View(forumComment);
        }

        // POST: Administration/ForumComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var forumComment = await _context.ForumComments.FindAsync(id);
            _context.ForumComments.Remove(forumComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumCommentExists(string id)
        {
            return _context.ForumComments.Any(e => e.Id == id);
        }
    }
}
