﻿using System;
using Android.Content;

namespace Badge.Plugin
{
    /**
	 * Shortcut Badger support for Nova Launcher.
	 * TeslaUnread must be installed.
	 * User: Gernot Pansy
	 * Date: 2014/11/03
	 * Time: 7:15
	 * 
	 * ported to C# by Jake Henning
	 */
    public class NovaBadgeProvider : BadgeProvider
    {
        private const string PackageId = "tag";
        private const string CountId = "count";
        private const string ContentId = "content://com.teslacoilsw.notifier/unread_count";

        public NovaBadgeProvider(Context context)
            : base(context)
        {
        }

        public override void SetBadge(int count)
        {
            try
            {
                var contentValues = new ContentValues();
                contentValues.Put(PackageId, string.Format("{0}/{1}", PackageName, MainActivityClassName));
                contentValues.Put(CountId, count);
                Context.ContentResolver.Insert(Android.Net.Uri.Parse(ContentId), contentValues);
            }
            catch (Java.Lang.IllegalArgumentException ex)
            {
                /* Fine, TeslaUnread is not installed. */
            }
            catch (Exception ex)
            {
                /* Some other error, possibly because the format of the ContentValues are incorrect. */
                throw new BadgesNotSupportedException(ex.Message);
            }
        }

        public override void RemoveBadge()
        {
            SetBadge(0);
        }
    }
}