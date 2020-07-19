import React from 'react';
import {
  Layout,
  Menu
} from 'antd';
import {
  HomeOutlined,
  AreaChartOutlined,
  PieChartOutlined,
} from '@ant-design/icons';
import './style.css';


export function Header({ name }) {
  const { Header } = Layout;
  return (
    <Header>
    <div className="logo" />
    <Menu theme="dark" mode="horizontal" defaultSelectedKeys={['2']}>
      <Menu.Item key="1"><HomeOutlined /></Menu.Item>
      <Menu.Item key="2"><AreaChartOutlined /></Menu.Item>
      <Menu.Item key="3"><PieChartOutlined /></Menu.Item>
    </Menu>
  </Header>
  );
}

export default Header;
