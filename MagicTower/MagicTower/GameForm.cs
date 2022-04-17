using System;
using System.Drawing;
using System.Windows.Forms;
using MagicTower.Model;

namespace MagicTower
{
    public partial class GameForm : Form
    {
        private GameModel gameModel;
        private PlayerView playerView;

        public GameForm()
        {
            InitializeComponent();
            gameModel = new GameModel(500, 500);
            playerView = new PlayerView(gameModel.Player);
        }

        protected override void OnLoad(EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            Text = "Magic Tower";
            DoubleBuffered = true;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            playerView.Draw(e.Graphics);
            Cursor.Current = new Cursor(@"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\cursor.cur");
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                gameModel.Player.Move(Direction.Left);
            // gameModel.MovePlayerTo(Directions.Left);
            if (e.KeyCode == Keys.D)
                gameModel.Player.Move(Direction.Right);
            if (e.KeyCode == Keys.W)
                gameModel.Player.Move(Direction.Up);
            if (e.KeyCode == Keys.S)
                gameModel.Player.Move(Direction.Down);
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs mouse)
        {
            if (mouse.X > gameModel.Player.PosX && playerView.imageDirection == Direction.Left
                || mouse.X < gameModel.Player.PosX && playerView.imageDirection == Direction.Right)
            {
                playerView.FlipImage();
                Invalidate();
            }
        }
    }

    public class PlayerView
    {
        private PlayerModel player;
        public readonly Image playerSprite;

        public Direction imageDirection { get; set; }
        // private Bitmap playerBitmap;

        public PlayerView(PlayerModel player)
        {
            this.player = player;
            playerSprite =
                Image.FromFile(@"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\player.png");
            imageDirection = Direction.Right;
            // playerBitmap = new Bitmap(playerSprite, new Size(16*4, 16*7));
        }

        public void Draw(Graphics e)
        {
            e.DrawImage(playerSprite, new Point(player.PosX, player.PosY));
            // e.DrawImage(playerBitmap, 100, 100);
        }

        public void FlipImage()
        {
            if (imageDirection == Direction.Right)
                imageDirection = Direction.Left;
            else
                imageDirection = Direction.Right;
            playerSprite.RotateFlip(RotateFlipType.Rotate180FlipY);
        }
    }
}