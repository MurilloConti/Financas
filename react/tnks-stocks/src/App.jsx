import React, { Component } from 'react';
import {
  BrowserRouter as Router,
  // Route,
  Switch,
} from 'react-router-dom';
import { Layout, Card, Row, Col } from 'antd';
import Header from './pages/Header/Header'
const { Content, Footer } = Layout;
class App extends Component {
  render() {
    return (
      <Router>
        <Layout style={{ minHeight: '100vh' }}>
            <Header />
          <Content style={{ padding: '0 50px' }}>
            <Switch>
              {/* <Route exact path="/" component={home} /> */}
            </Switch>
            <Row gutter={16}>
              <Col span={8} offset={4}>
                <Card title="Card title" bordered={false}>
                  Card content
        </Card>
              </Col>
              <Col span={8}>
                <Card title="Card title" bordered={false}>
                  Card content
        </Card>
              </Col>
            </Row>
            <Row gutter={16}>
            <Col span={16} offset={4}>
                <Card title="Card title" bordered={false}>
                  Card content
        </Card>
              </Col>
            </Row>
          </Content>
            <Footer style={{ textAlign: 'center' }}>Ant Design ©2018 Created by Ant UED</Footer>
        </Layout>
      </Router>
    );
  }
}

export default App;
