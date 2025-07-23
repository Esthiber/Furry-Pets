namespace PawfectMatch.Constants
{
    public static class Icons
    {
        // Method to get all icon names
        public static List<string> GetAllIcons()
        {
            var allIcons = new List<string>();
            var categories = typeof(Icons).GetNestedTypes(); // Gets nested classes: Interface, User, Animals, etc.

            foreach (var category in categories)
            {
                var icons = category.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                foreach (var icon in icons)
                {
                    allIcons.Add(icon.GetValue(null)!.ToString()!);
                }
            }
            return allIcons;
        }

        public static class Interface
        {
            // Basic UI Elements
            public const string House = "house";
            public const string HouseDoorFill = "house-door-fill";
            public const string GearFill = "gear-fill";
            public const string Gear = "gear";
            public const string QuestionCircle = "question-circle";
            public const string QuestionLg = "question-lg";
            public const string Search = "search";
            public const string Plus = "plus";
            public const string PlusCircle = "plus-circle";
            public const string PlusLg = "plus-lg";
            public const string Dash = "dash";
            public const string DashCircle = "dash-circle";
            public const string DashLg = "dash-lg";
            public const string X = "x";
            public const string XCircle = "x-circle";
            public const string XLg = "x-lg";
            public const string Check = "check";
            public const string CheckCircle = "check-circle";
            public const string CheckLg = "check-lg";
            public const string Three = "three-dots";
            public const string ThreeVertical = "three-dots-vertical";
        }

        public static class User
        {
            public const string Person = "person";
            public const string PersonCircle = "person-circle";
            public const string PersonCheckFill = "person-check-fill";
            public const string PersonFill = "person-fill";
            public const string PersonAdd = "person-add";
            public const string PersonX = "person-x";
            public const string People = "people";
            public const string PeopleFill = "people-fill";
            public const string BoxArrowInRight = "box-arrow-in-right";
            public const string BoxArrowLeft = "box-arrow-left";
            public const string BoxArrowUp = "box-arrow-up";
            public const string Key = "key";
            public const string KeyFill = "key-fill";
            public const string Lock = "lock";
            public const string LockFill = "lock-fill";
            public const string Unlock = "unlock";
            public const string UnlockFill = "unlock-fill";
            public const string Shield = "shield";
            public const string ShieldLock = "shield-lock";
            public const string ShieldCheck = "shield-check";
        }

        public static class Animals
        {
            public const string Dribbble = "dribbble";
            public const string PawPrint = "paw-print";
            public const string Heart = "heart";
            public const string HeartFill = "heart-fill";
            public const string HeartHalf = "heart-half";
            public const string HeartPulse = "heart-pulse";
            public const string Star = "star";
            public const string StarFill = "star-fill";
            public const string StarHalf = "star-half";
            public const string Bug = "bug";
            public const string BugFill = "bug-fill";
        }

        public static class Commerce
        {
            public const string Cart = "cart";
            public const string CartFill = "cart-fill";
            public const string CartPlus = "cart-plus";
            public const string CartCheck = "cart-check";
            public const string CartX = "cart-x";
            public const string Bag = "bag";
            public const string BagFill = "bag-fill";
            public const string BagPlus = "bag-plus";
            public const string BagCheck = "bag-check";
            public const string Shop = "shop";
            public const string ShopWindow = "shop-window";
            public const string Tag = "tag";
            public const string TagFill = "tag-fill";
            public const string Tags = "tags";
            public const string TagsFill = "tags-fill";
            public const string Box = "box";
            public const string BoxSeam = "box-seam";
            public const string Cash = "cash";
            public const string CashStack = "cash-stack";
            public const string CreditCard = "credit-card";
            public const string CreditCard2Front = "credit-card-2-front";
            public const string Receipt = "receipt";
        }

        public static class Admin
        {
            public const string DatabaseFillGear = "database-fill-gear";
            public const string Database = "database";
            public const string DatabaseFill = "database-fill";
            public const string Gitlab = "gitlab";
            public const string Grid = "grid";
            public const string GridFill = "grid-fill";
            public const string Grid1x2 = "grid-1x2";
            public const string Grid3x3 = "grid-3x3";
            public const string List = "list";
            public const string ListUl = "list-ul";
            public const string ListOl = "list-ol";
            public const string ListCheck = "list-check";
            public const string Table = "table";
            public const string PencilSquare = "pencil-square";
            public const string Pencil = "pencil";
            public const string TrashFill = "trash-fill";
            public const string Trash = "trash";
            public const string Trash2 = "trash2";
            public const string Trash3 = "trash3";
        }

        public static class Social
        {
            public const string Facebook = "facebook";
            public const string Instagram = "instagram";
            public const string Youtube = "youtube";
            public const string Twitter = "twitter";
            public const string LinkedIn = "linkedin";
            public const string Github = "github";
            public const string Share = "share";
            public const string ShareFill = "share-fill";
            public const string Chat = "chat";
            public const string ChatFill = "chat-fill";
            public const string ChatDots = "chat-dots";
            public const string ChatDotsFill = "chat-dots-fill";
        }

        public static class Feedback
        {
            public const string InfoCircle = "info-circle";
            public const string InfoCircleFill = "info-circle-fill";
            public const string ExclamationCircle = "exclamation-circle";
            public const string ExclamationCircleFill = "exclamation-circle-fill";
            public const string ExclamationTriangle = "exclamation-triangle";
            public const string ExclamationTriangleFill = "exclamation-triangle-fill";
            public const string CheckCircleFill = "check-circle-fill";
            public const string XCircleFill = "x-circle-fill";
            public const string Emoji = "emoji-smile";
            public const string EmojiNeutral = "emoji-neutral";
            public const string EmojiFrown = "emoji-frown";
        }

        public static class Files
        {
            public const string File = "file";
            public const string FileEarmark = "file-earmark";
            public const string FilePdf = "file-pdf";
            public const string FileWord = "file-word";
            public const string FileExcel = "file-excel";
            public const string FileImage = "file-image";
            public const string FileText = "file-text";
            public const string FileCode = "file-code";
            public const string Folder = "folder";
            public const string FolderFill = "folder-fill";
            public const string FolderPlus = "folder-plus";
            public const string FolderMinus = "folder-minus";
        }

        public static class Media
        {
            public const string Play = "play";
            public const string PlayFill = "play-fill";
            public const string Pause = "pause";
            public const string PauseFill = "pause-fill";
            public const string Stop = "stop";
            public const string StopFill = "stop-fill";
            public const string Image = "image";
            public const string ImageFill = "image-fill";
            public const string Camera = "camera";
            public const string CameraFill = "camera-fill";
            public const string Video = "camera-video";
            public const string VideoFill = "camera-video-fill";
            public const string Music = "music-note";
            public const string MusicFill = "music-note-fill";
        }

        public static class Arrows
        {
            public const string ArrowUp = "arrow-up";
            public const string ArrowDown = "arrow-down";
            public const string ArrowLeft = "arrow-left";
            public const string ArrowRight = "arrow-right";
            public const string ArrowClockwise = "arrow-clockwise";
            public const string ArrowCounterclockwise = "arrow-counterclockwise";
            public const string ChevronUp = "chevron-up";
            public const string ChevronDown = "chevron-down";
            public const string ChevronLeft = "chevron-left";
            public const string ChevronRight = "chevron-right";
            public const string CaretUp = "caret-up";
            public const string CaretDown = "caret-down";
            public const string CaretLeft = "caret-left";
            public const string CaretRight = "caret-right";
        }
    }
}
