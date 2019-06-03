using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Firebase.Firestore;
using Firebase;
using Java.Util;
using Android.Content;
using System.Threading.Tasks;

namespace UGAndroidCloud
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText Type;
        EditText Breed;
        EditText Name;
        EditText Info;
        EditText AllInfo;
        Button saveButton;
        Button refreshButton;
        FirebaseFirestore database;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ConnectViews();
            database = GetDataBase(); // z tym moze byc problem, musialem deklarowac typ, powinien byc wczesniej?
            AllInfo.Text = GetAllCurrentInfo();
        }

        void ConnectViews()
        {
            Type = (EditText)FindViewById(Resource.Id.Type);
            Breed = (EditText)FindViewById(Resource.Id.Breed);
            Name = (EditText)FindViewById(Resource.Id.Name);
            Info = (EditText)FindViewById(Resource.Id.Info);
            AllInfo = (EditText)FindViewById(Resource.Id.AllInfo);
            saveButton = (Button)FindViewById(Resource.Id.saveButton);
            refreshButton = (Button)FindViewById(Resource.Id.refreshButton);
            
            saveButton.Click += SaveButton_Click;
            refreshButton.Click += RefreshButton_Click;
        }

        public string GetAllCurrentInfo()
        {
            return "";
        }

        //public async Task<string> GetAllCurrentInfo()
        //{
        //    DocumentReference docRef = database.Collection("cities").Document("SF");
        //    DocumentSnapshot snapshot = await docRef.SnapshotAsync();
        //    return "";
        //}

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            HashMap map = new HashMap();
            map.Put("Type", Type.Text);
            map.Put("Breed", Breed.Text);
            map.Put("Name", Name.Text);
            map.Put("Info", Info.Text);

            DocumentReference docRef = database.Collection("Infos").Document();
            docRef.Set(map);
        }

        private void RefreshButton_Click(object sender, System.EventArgs e)
        {
            AllInfo.Text = GetAllCurrentInfo();
        }

        public FirebaseFirestore GetDataBase()
        {
            FirebaseFirestore database;

            var options = new FirebaseOptions.Builder().SetProjectId("TU MA BYC PROJECT_ID Z PLIKU GOOGLE-SERVICES.JSON")
                .SetApplicationId("to samo")
                .SetApiKey("CURRENT_KEY z tego samego pliku")
                .SetDatabaseUrl("FIREBASE_URL z tego samego pliku")
                .SetStorageBucket("STORAGE_BUCKET z tego samego").
                Build();

            var app = FirebaseApp.InitializeApp(this, options);
            database = FirebaseFirestore.GetInstance(app);

            return database;
        }
    }
}