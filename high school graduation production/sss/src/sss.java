//シューティング
import com.nttdocomo.ui.*;

public class sss extends IApplication {
	sssCanvas c = null;
	public void start() {
		c = new sssCanvas(this);
		Display.setCurrent(c);
	}
	void endProc(){
		if(c != null) c.endProc();
		terminate();
	}
}

class sssCanvas extends Canvas implements Runnable {
	MediaImage mi;
	Image img;
	int o=2,u=1,j=5,h=4;
	int score=0;
	int _sp04=0,_sp05=0,_sp06,_sp07=0;
	int _sp14=0,_sp15=0,_sp16,_sp17=0;
	int _sp84=0,_sp85=0,_sp86,_sp87=0;
	int _sp94=0,_sp95=0,_sp96,_sp97=0;
	int _red=0,_yellow=0,_green=0;
	int rek4=0,rek5=0,rek6=0,rek7=0;
	int reb4=0,reb5=0,reb6=0,reb7=0;
	int ye4=0,ye5=0,ye6=0,ye7=0;
	int gr4=0,gr5=0,gr6=0,gr7=0;
	SpriteSet spSet;
	Sprite[] sp;
	int keyState, bomState;
	sss parent;
	// deviceのCanvasサイズに合わせて幅、高さを取得
	int C_Width = getWidth();
	int C_Height = getHeight();
	Thread gameLoop = null;
	sssCanvas(sss p){
		sp = new Sprite[15];
		keyState = 0;
		bomState = 0;
		parent   = p;
		//画像("zako.gif")を読み込む
		mi = MediaManager.getImage("resource:///zako3.gif");
		try{
			mi.use();
			img = mi.getImage();
			//弾のスプライトの生成...4
			sp[4] = new Sprite(img,55,0,2,6);
			sp[4].setLocation(0,-40);
			//弾2のスプライトの生成...5
			sp[5] = new Sprite(img,50,0,3,10);
			sp[5].setLocation(0,-40);
			//弾3のスプライトの生成...6
			sp[6] = new Sprite(img,60,0,2,6);
			sp[6].setLocation(0,-40);
			//弾4のスプライトの生成...7
			sp[7] = new Sprite(img,80,0,4,15);
			sp[7].setLocation(0,-40);
			/////////////////////////////////////////
			//敵スプライトの生成...0
			sp[0] = new Sprite(img,100,100,48,48);
			sp[0].setLocation(-300+C_Width+10,50);
			//敵スプライトの生成...1
			sp[1] = new Sprite(img,150,100,48,48);
			sp[1].setLocation(C_Width,0);
			//敵スプライトの生成...8
			sp[8] = new Sprite(img,200,100,48,48);
			sp[8].setLocation(C_Width+20,100);
			//敵スプライトの生成...9
			sp[9] = new Sprite(img,0,0,48,48);
			sp[9].setLocation(C_Width+100,30);
			//////////////////////////////////////////
			//敵の弾の生成...10
			sp[10] = new Sprite(img,55,0,2,6);
			sp[10].setLocation(0,-40);
			/////////////////////////////////////////
			//自機スプライトの生成...2
			sp[2] = new Sprite(img,0,50,48,48);
			sp[2].setLocation(106,C_Height-80);
			// スプライトに登録
			spSet = new SpriteSet(sp);
			// スレッドの準備
			gameLoop = new Thread(this);
			// スレッドの開始
			gameLoop.start();
		}
		catch(Exception e) {
			e.printStackTrace();
			p.endProc();
		}
	}

	/** ゲームループメソッド*/
	public void run(){
		// オフスクリーンバッファの作成
		Graphics g = getGraphics();
		// タイマーの初期設定
		long tm = System.currentTimeMillis();
		// ゲーム・ループ
		while(gameLoop != null){
			// 描画と表示
			paintFrame(g);
			try{
				// スレッドの一時停止(40ミリ秒)
				tm -= System.currentTimeMillis();
				if(tm>-40)gameLoop.sleep(40+tm);
				tm = System.currentTimeMillis();
			}
			catch(Exception e) {
				e.printStackTrace();
				break;
			}
			// 描画座標の更新
			renew();
		}
		//終了処理
		parent.endProc();
	}

	/** 描画を行うメソッド */
	void paintFrame(Graphics g){
		// オフスクリーンのバッファヘノ描画開始
		g.lock();
		// 背景色による塗りつぶし
		setBackground(0x000000ff);
		g.clearRect(0,0,C_Width,C_Height);
		// 画像の描画
		g.drawSpriteSet(spSet);
		//スコアの表示
		g.setColor(0x01ffffff);
		g.drawString("スコア:"+String.valueOf(score),2,230);
		//敵を何体倒したかの表示
		g.drawString("赤:"+String.valueOf(_red),90,230);
		g.drawString("黄:"+String.valueOf(_yellow),140,230);
		g.drawString("緑:"+String.valueOf(_green),190,230);
		/////////////////////////////////////////////////////////////////////////////////////////////////
 		if((score>499999) && (score<501000))g.drawString("＊スコア半分達成おめでとう＊",2,215);
		else if(score==999999)g.drawString("＊スコア達成おめでとう＊",2,215);
		/////////////////////////////////////////////////////////////////////////////////////////////////
		//オフスクリーン・バッファのフラッシュ
		g.unlock(true);
	}

	/** コンパイルを通すための空定義 */
	public void paint(Graphics g){}
	/** 描画する座標の更新を行うメソッド */
	void renew(){
		int x,y;
		int _p;
		int p=105;
		_p=p;
		//ゲーム中
		//キー入力状態の取得
		int ks = getKeypadState();

		//1～3キーの押下で弾を変えて発射！...1キー
		if((ks & (1 << Display.KEY_1)) != 0){
			if(sp[4].getY() <=-40){
				x = sp[2].getX()+13;
				y = sp[2].getY();
				sp[4].setLocation(x,y);
			}
			//System.out.println("レーザー1");
		}
		//1～3キーの押下で弾を変えて発射！...2キー
		if((ks & (1 << Display.KEY_2)) != 0) {
			if(sp[5].getY() <=-40){
				x = sp[2].getX()+22;
				y = sp[2].getY();
				sp[5].setLocation(x,y);
			}
			//System.out.println("レーザー2");
		}
		//1～3キーの押下で弾を変えて発射！...3キー
		if((ks & (1 << Display.KEY_3)) != 0){
			if(sp[6].getY() <=-40){
				x = sp[2].getX()+30;
				y = sp[2].getY();
				sp[6].setLocation(x,y);
			}
			//System.out.println("レーザー3");
		}
		//隠しキー...123キー
		if((((ks & (1 << Display.KEY_1)) != 0) &&
				((ks & (1 << Display.KEY_3)) != 0)) &&((ks & (1 << Display.KEY_2)) != 0)){ 
			if((((ks & (1 << Display.KEY_UP)) != 0) && ((keyState & (1 << Display.KEY_UP)) == 0))||
			  (((ks & (1 << Display.KEY_DOWN)) != 0) && ((keyState & (1 << Display.KEY_DOWN)) == 0))||
			  (((ks & (1 << Display.KEY_5)) != 0) && ((keyState & (1 << Display.KEY_5)) == 0))){
				if(sp[7].getY() <=-40){
					x = sp[2].getX()+22;
					y = sp[2].getY();
					sp[7].setLocation(x,y);
				}
			}
		}

		//左キーが押されたらx減算
		if(((ks & (1 << Display.KEY_LEFT)) != 0)){
			if(sp[2].getX() > 1){
				if(sp[2].getX() <= 240){
					x = sp[2].getX()-5;
					sp[2].setLocation(x,C_Height-80);				
				}
				//System.out.println("左");
			}
		}
		//右キーが押されたらx加算
		if(((ks & (1 << Display.KEY_RIGHT)) != 0)){
			if(sp[2].getX() < 240 - 49){ 
				if(sp[2].getX() >= 0){
					x = sp[2].getX()+5;
					sp[2].setLocation(x,C_Height-80);
					_p += 5;
				}
				//System.out.println("右");
			}
		}
		/*
		//上キーが押されたらy減算
		if(((ks & (1 << Display.KEY_UP)) != 0)){
			if(sp[2].getY() >= 5){
				y = sp[2].getY()-5;
				sp[2].setLocation(C_Width,y);
			}
			//System.out.println("上");
		}
		//下キーが押されたらy加算
		if(((ks & (1 << Display.KEY_DOWN)) != 0)){
			if(sp[2].getY() >= 5){
				y = sp[10].getY()+5;
				sp[2].setLocation(C_Width,y);
			}
			//System.out.println("下");
		}
		*/
		//弾の移動...4
		if(sp[4].getY() > -40){
			x = sp[4].getX() -4;
			y = sp[4].getY() -8;
			sp[4].setLocation(x,y);
		}
		//弾の移動...5
		if(sp[5].getY() > -40){
			x = sp[5].getX();
			y = sp[5].getY() -12;
			sp[5].setLocation(x,y);
		}
		//弾の移動...6
		if(sp[6].getY() > -40){
			x = sp[6].getX() +4;
			y = sp[6].getY() -8;
			sp[6].setLocation(x,y);
		}
		//弾の移動...7
		if(sp[7].getY() > -40){
			x = sp[7].getX();
			y = sp[7].getY() -18;
			sp[7].setLocation(x,y);
		}

		//敵の移動...0
		if(sp[0].getY() > -40){
			x = sp[0].getX() +u;
			y = sp[0].getY();
			sp[0].setLocation(x,y);
		}else{
			sp[0].setLocation(C_Width,75);
		}
		//敵の移動...1
		if(sp[1].getY() > -40){
			x = sp[1].getX() -o;
			y = sp[1].getY();
			sp[1].setLocation(x,y);
		}else{
			sp[1].setLocation(C_Width,45);		
		}
		//敵の移動...8
		if(sp[8].getY() > -40){
			x = sp[8].getX() -j;
			y = sp[8].getY();
			sp[8].setLocation(x,y);
		}else{
			sp[8].setLocation(C_Width,45);		
		}

		
		//敵の移動...9
		if(sp[9].getY() > -40){
			x = sp[9].getX() -h;
			y = sp[9].getY();
			sp[9].setLocation(x,y);
		}else{
			sp[9].setLocation(C_Width,45);		
		}
		

		//敵の弾発射...10
		if(sp[10].getY() > -40){
			x = sp[10].getX();
			y = sp[10].getY() +7;
			sp[10].setLocation(x,y);
		}
		//当たり判定を必要とするスプライトの指定
		spSet.setCollisionOf(0);
		spSet.setCollisionOf(1);
		spSet.setCollisionOf(2);
		spSet.setCollisionOf(4);
		spSet.setCollisionOf(5);
		spSet.setCollisionOf(6);
		spSet.setCollisionOf(7);
		spSet.setCollisionOf(8);
		spSet.setCollisionOf(9);
		spSet.setCollisionOf(10);

		//自機の当たり判定
		if(spSet.isCollision(2,10)){
			sp[2].setLocation(106,C_Height-60);
			sp[10].setLocation(300,-40);
		}

		//敵0の当たり判定（スコアも）
		if(spSet.isCollision(4,0)){
			sp[0].setLocation(-300+C_Width,50);
			sp[4].setLocation(0,-40);
			_sp04=30;
			ye4=1;
			if(u<=15)u=u+1;
		}else{
			_sp04=0;
			ye4=0;
		}
		if(spSet.isCollision(5,0)){
			sp[0].setLocation(-300+C_Width,50);
			sp[5].setLocation(0,-40);
			_sp05=30;
			ye5=1;
			if(u<=15)u=u+1;
		}else{
			_sp05=0;
			ye5=0;
		}
		if(spSet.isCollision(6,0)){
			sp[0].setLocation(-300+C_Width,50);
			sp[6].setLocation(0,-40);
			_sp06=30;
			ye6=1;
			if(u<=15)u=u+1;
		}else{
			_sp06=0;
			ye6=0;
		}
		if(spSet.isCollision(7,0)){
			sp[0].setLocation(-300+C_Width,50);
			sp[7].setLocation(0,-40);
			_sp07=80;
			ye7=1;
			u=1;
		}else{
			_sp07=0;
			ye7=0;
		}
		if(sp[0].getX()>340){
			sp[0].setLocation(-300+C_Width,50);
			if(u<=15)u=u+1;
		}
		//敵1の当たり判定（スコアも）
		if(spSet.isCollision(4,1)){
			sp[1].setLocation(C_Width,0);
			sp[4].setLocation(0,-40);
			_sp14=20;
			rek4=1;
			if(o<=7)o=o+1;
		}else{
			_sp14=0;
			rek4=0;
		}
		if(spSet.isCollision(5,1)){
			sp[1].setLocation(C_Width,0);
			sp[5].setLocation(0,-40);
			_sp15=20;
			rek5=1;
			if(o<=7)o=o+1;
		}else{
			_sp15=0;
			rek5=0;
		}
		if(spSet.isCollision(6,1)){
			sp[1].setLocation(C_Width,0);
			sp[6].setLocation(0,-40);
			_sp16=20;
			rek6=1;
			if(o<=7)o=o+1;
		}else{
			_sp16=0;
			rek6=0;
		}
		if(spSet.isCollision(7,1)){
			sp[1].setLocation(C_Width,0);
			sp[7].setLocation(0,-40);
			_sp17=60;
			rek7=1;
			o=2;
		}else{
			_sp17=0;
			rek7=0;
		}
		if(sp[1].getX()<-150){
			sp[1].setLocation(C_Width,0);
			if(o<=7)o=o+1;
		}
		//敵8の当たり判定（スコアも）
		if(spSet.isCollision(4,8)){
			sp[8].setLocation(C_Width,100);
			sp[4].setLocation(0,-40);
			_sp84=10;
			gr4=1;
			if(j<=10)j=j+1;
		}else{
			_sp84=0;
			gr4=0;
		}
		if(spSet.isCollision(5,8)){
			sp[8].setLocation(C_Width,100);
			sp[5].setLocation(0,-40);
			_sp85=10;
			gr5=1;
			if(j<=11)j=j+1;
		}else{
			_sp85=0;
			gr5=0;
		}
		if(spSet.isCollision(6,8)){
			sp[8].setLocation(C_Width,100);
			sp[6].setLocation(0,-40);
			_sp86=10;
			gr6=1;
			if(j<=11)j=j+1;
		}else{
			_sp86=0;
			gr6=0;
		}
		if(spSet.isCollision(7,8)){
			sp[8].setLocation(C_Width,100);
			sp[7].setLocation(0,-40);
			_sp87=40;
			gr7=1;
			j=5;
		}else{
			_sp87=0;
			gr7=0;
		}
		if(sp[8].getX()<-150){
			sp[8].setLocation(C_Width,100);
			if(j<=11)j=j+1;
		}
		
		//敵9の当たり判定（スコアも）
		if(spSet.isCollision(4,9)){
			sp[9].setLocation(100+C_Width,30);
			sp[4].setLocation(0,-40);
			_sp94=-15;
			reb4=1;
			if(h<=16)h=h+2;
		}else{
			_sp94=0;
			reb4=0;
		}
		if(spSet.isCollision(5,9)){
			sp[9].setLocation(100+C_Width,30);
			sp[5].setLocation(0,-40);
			_sp95=-15;
			reb5=1;
			if(h<=16)h=h+2;
		}else{
			_sp95=0;
			reb5=0;
		}
		if(spSet.isCollision(6,9)){
			sp[9].setLocation(100+C_Width,30);
			sp[6].setLocation(0,-40);
			_sp96=-15;
			reb6=1;
			if(h<=16)h=h+2;
		}else{
			_sp96=0;
			reb6=0;
		}
		if(spSet.isCollision(7,9)){
			sp[9].setLocation(100+C_Width,30);
			sp[7].setLocation(0,-40);
			_sp97=-5;
			reb7=1;
			h=3;
		}else{
			_sp97=0;
			reb7=0;
		}
		if(sp[9].getX()<-150){
			sp[9].setLocation(100+C_Width,30);
			if(h<=18)h=h+2;
		}
		
		//キー入力状態の保存
		keyState = ks;
		//スコア計算
		if((score<999999) && (score>-1)) score=score+_sp04+_sp05+_sp06+_sp07+_sp14+_sp15+_sp16+_sp17+_sp84+_sp85+_sp86+_sp87+_sp94+_sp95+_sp96+_sp97;
		else if(score>999999)	score=999999;
		else if(score<-1)	score=0;
		//敵の倒した数を計算
		if(_red<9999)_red=_red+rek4+rek5+rek6+rek7+reb4+reb5+reb6+reb7;
		if(_yellow<9999)_yellow=_yellow+ye4+ye5+ye6+ye7;
		if(_green<9999)_green=_green+gr4+gr5+gr6+gr7;
	}
	/**終了処理を行なうメソッド*/
	void endProc(){
		gameLoop = null;
		try{
			if(img != null){
				img.dispose();
				img = null;
			}
			if(mi != null){
				mi.unuse();
				mi.dispose();
				mi =null;
			}
		}
		catch(Exception e) {
			e.printStackTrace();
		}
		System.gc();
	}
}