//�V���[�e�B���O
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
	// device��Canvas�T�C�Y�ɍ��킹�ĕ��A�������擾
	int C_Width = getWidth();
	int C_Height = getHeight();
	Thread gameLoop = null;
	sssCanvas(sss p){
		sp = new Sprite[15];
		keyState = 0;
		bomState = 0;
		parent   = p;
		//�摜("zako.gif")��ǂݍ���
		mi = MediaManager.getImage("resource:///zako3.gif");
		try{
			mi.use();
			img = mi.getImage();
			//�e�̃X�v���C�g�̐���...4
			sp[4] = new Sprite(img,55,0,2,6);
			sp[4].setLocation(0,-40);
			//�e2�̃X�v���C�g�̐���...5
			sp[5] = new Sprite(img,50,0,3,10);
			sp[5].setLocation(0,-40);
			//�e3�̃X�v���C�g�̐���...6
			sp[6] = new Sprite(img,60,0,2,6);
			sp[6].setLocation(0,-40);
			//�e4�̃X�v���C�g�̐���...7
			sp[7] = new Sprite(img,80,0,4,15);
			sp[7].setLocation(0,-40);
			/////////////////////////////////////////
			//�G�X�v���C�g�̐���...0
			sp[0] = new Sprite(img,100,100,48,48);
			sp[0].setLocation(-300+C_Width+10,50);
			//�G�X�v���C�g�̐���...1
			sp[1] = new Sprite(img,150,100,48,48);
			sp[1].setLocation(C_Width,0);
			//�G�X�v���C�g�̐���...8
			sp[8] = new Sprite(img,200,100,48,48);
			sp[8].setLocation(C_Width+20,100);
			//�G�X�v���C�g�̐���...9
			sp[9] = new Sprite(img,0,0,48,48);
			sp[9].setLocation(C_Width+100,30);
			//////////////////////////////////////////
			//�G�̒e�̐���...10
			sp[10] = new Sprite(img,55,0,2,6);
			sp[10].setLocation(0,-40);
			/////////////////////////////////////////
			//���@�X�v���C�g�̐���...2
			sp[2] = new Sprite(img,0,50,48,48);
			sp[2].setLocation(106,C_Height-80);
			// �X�v���C�g�ɓo�^
			spSet = new SpriteSet(sp);
			// �X���b�h�̏���
			gameLoop = new Thread(this);
			// �X���b�h�̊J�n
			gameLoop.start();
		}
		catch(Exception e) {
			e.printStackTrace();
			p.endProc();
		}
	}

	/** �Q�[�����[�v���\�b�h*/
	public void run(){
		// �I�t�X�N���[���o�b�t�@�̍쐬
		Graphics g = getGraphics();
		// �^�C�}�[�̏����ݒ�
		long tm = System.currentTimeMillis();
		// �Q�[���E���[�v
		while(gameLoop != null){
			// �`��ƕ\��
			paintFrame(g);
			try{
				// �X���b�h�̈ꎞ��~(40�~���b)
				tm -= System.currentTimeMillis();
				if(tm>-40)gameLoop.sleep(40+tm);
				tm = System.currentTimeMillis();
			}
			catch(Exception e) {
				e.printStackTrace();
				break;
			}
			// �`����W�̍X�V
			renew();
		}
		//�I������
		parent.endProc();
	}

	/** �`����s�����\�b�h */
	void paintFrame(Graphics g){
		// �I�t�X�N���[���̃o�b�t�@�w�m�`��J�n
		g.lock();
		// �w�i�F�ɂ��h��Ԃ�
		setBackground(0x000000ff);
		g.clearRect(0,0,C_Width,C_Height);
		// �摜�̕`��
		g.drawSpriteSet(spSet);
		//�X�R�A�̕\��
		g.setColor(0x01ffffff);
		g.drawString("�X�R�A:"+String.valueOf(score),2,230);
		//�G�����̓|�������̕\��
		g.drawString("��:"+String.valueOf(_red),90,230);
		g.drawString("��:"+String.valueOf(_yellow),140,230);
		g.drawString("��:"+String.valueOf(_green),190,230);
		/////////////////////////////////////////////////////////////////////////////////////////////////
 		if((score>499999) && (score<501000))g.drawString("���X�R�A�����B�����߂łƂ���",2,215);
		else if(score==999999)g.drawString("���X�R�A�B�����߂łƂ���",2,215);
		/////////////////////////////////////////////////////////////////////////////////////////////////
		//�I�t�X�N���[���E�o�b�t�@�̃t���b�V��
		g.unlock(true);
	}

	/** �R���p�C����ʂ����߂̋��` */
	public void paint(Graphics g){}
	/** �`�悷����W�̍X�V���s�����\�b�h */
	void renew(){
		int x,y;
		int _p;
		int p=105;
		_p=p;
		//�Q�[����
		//�L�[���͏�Ԃ̎擾
		int ks = getKeypadState();

		//1�`3�L�[�̉����Œe��ς��Ĕ��ˁI...1�L�[
		if((ks & (1 << Display.KEY_1)) != 0){
			if(sp[4].getY() <=-40){
				x = sp[2].getX()+13;
				y = sp[2].getY();
				sp[4].setLocation(x,y);
			}
			//System.out.println("���[�U�[1");
		}
		//1�`3�L�[�̉����Œe��ς��Ĕ��ˁI...2�L�[
		if((ks & (1 << Display.KEY_2)) != 0) {
			if(sp[5].getY() <=-40){
				x = sp[2].getX()+22;
				y = sp[2].getY();
				sp[5].setLocation(x,y);
			}
			//System.out.println("���[�U�[2");
		}
		//1�`3�L�[�̉����Œe��ς��Ĕ��ˁI...3�L�[
		if((ks & (1 << Display.KEY_3)) != 0){
			if(sp[6].getY() <=-40){
				x = sp[2].getX()+30;
				y = sp[2].getY();
				sp[6].setLocation(x,y);
			}
			//System.out.println("���[�U�[3");
		}
		//�B���L�[...123�L�[
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

		//���L�[�������ꂽ��x���Z
		if(((ks & (1 << Display.KEY_LEFT)) != 0)){
			if(sp[2].getX() > 1){
				if(sp[2].getX() <= 240){
					x = sp[2].getX()-5;
					sp[2].setLocation(x,C_Height-80);				
				}
				//System.out.println("��");
			}
		}
		//�E�L�[�������ꂽ��x���Z
		if(((ks & (1 << Display.KEY_RIGHT)) != 0)){
			if(sp[2].getX() < 240 - 49){ 
				if(sp[2].getX() >= 0){
					x = sp[2].getX()+5;
					sp[2].setLocation(x,C_Height-80);
					_p += 5;
				}
				//System.out.println("�E");
			}
		}
		/*
		//��L�[�������ꂽ��y���Z
		if(((ks & (1 << Display.KEY_UP)) != 0)){
			if(sp[2].getY() >= 5){
				y = sp[2].getY()-5;
				sp[2].setLocation(C_Width,y);
			}
			//System.out.println("��");
		}
		//���L�[�������ꂽ��y���Z
		if(((ks & (1 << Display.KEY_DOWN)) != 0)){
			if(sp[2].getY() >= 5){
				y = sp[10].getY()+5;
				sp[2].setLocation(C_Width,y);
			}
			//System.out.println("��");
		}
		*/
		//�e�̈ړ�...4
		if(sp[4].getY() > -40){
			x = sp[4].getX() -4;
			y = sp[4].getY() -8;
			sp[4].setLocation(x,y);
		}
		//�e�̈ړ�...5
		if(sp[5].getY() > -40){
			x = sp[5].getX();
			y = sp[5].getY() -12;
			sp[5].setLocation(x,y);
		}
		//�e�̈ړ�...6
		if(sp[6].getY() > -40){
			x = sp[6].getX() +4;
			y = sp[6].getY() -8;
			sp[6].setLocation(x,y);
		}
		//�e�̈ړ�...7
		if(sp[7].getY() > -40){
			x = sp[7].getX();
			y = sp[7].getY() -18;
			sp[7].setLocation(x,y);
		}

		//�G�̈ړ�...0
		if(sp[0].getY() > -40){
			x = sp[0].getX() +u;
			y = sp[0].getY();
			sp[0].setLocation(x,y);
		}else{
			sp[0].setLocation(C_Width,75);
		}
		//�G�̈ړ�...1
		if(sp[1].getY() > -40){
			x = sp[1].getX() -o;
			y = sp[1].getY();
			sp[1].setLocation(x,y);
		}else{
			sp[1].setLocation(C_Width,45);		
		}
		//�G�̈ړ�...8
		if(sp[8].getY() > -40){
			x = sp[8].getX() -j;
			y = sp[8].getY();
			sp[8].setLocation(x,y);
		}else{
			sp[8].setLocation(C_Width,45);		
		}

		
		//�G�̈ړ�...9
		if(sp[9].getY() > -40){
			x = sp[9].getX() -h;
			y = sp[9].getY();
			sp[9].setLocation(x,y);
		}else{
			sp[9].setLocation(C_Width,45);		
		}
		

		//�G�̒e����...10
		if(sp[10].getY() > -40){
			x = sp[10].getX();
			y = sp[10].getY() +7;
			sp[10].setLocation(x,y);
		}
		//�����蔻���K�v�Ƃ���X�v���C�g�̎w��
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

		//���@�̓����蔻��
		if(spSet.isCollision(2,10)){
			sp[2].setLocation(106,C_Height-60);
			sp[10].setLocation(300,-40);
		}

		//�G0�̓����蔻��i�X�R�A���j
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
		//�G1�̓����蔻��i�X�R�A���j
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
		//�G8�̓����蔻��i�X�R�A���j
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
		
		//�G9�̓����蔻��i�X�R�A���j
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
		
		//�L�[���͏�Ԃ̕ۑ�
		keyState = ks;
		//�X�R�A�v�Z
		if((score<999999) && (score>-1)) score=score+_sp04+_sp05+_sp06+_sp07+_sp14+_sp15+_sp16+_sp17+_sp84+_sp85+_sp86+_sp87+_sp94+_sp95+_sp96+_sp97;
		else if(score>999999)	score=999999;
		else if(score<-1)	score=0;
		//�G�̓|���������v�Z
		if(_red<9999)_red=_red+rek4+rek5+rek6+rek7+reb4+reb5+reb6+reb7;
		if(_yellow<9999)_yellow=_yellow+ye4+ye5+ye6+ye7;
		if(_green<9999)_green=_green+gr4+gr5+gr6+gr7;
	}
	/**�I���������s�Ȃ����\�b�h*/
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